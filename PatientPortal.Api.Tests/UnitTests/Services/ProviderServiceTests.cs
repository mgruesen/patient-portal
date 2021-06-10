using PatientPortal.Services;
using Xunit;
using PatientPortal.Api.Mappers;
using System;
using PatientPortal.Api.Domain;
using System.Linq;
using System.Collections.Generic;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Tests.UnitTests.Services
{
    public class ProviderServiceTests : TestDatabaseFixture
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IProviderMapper _providerMapper;
        private readonly ProviderService _sut;

        public ProviderServiceTests(TestDatabaseContext dbContext)
            : base(dbContext)
        {
            _dbContext = Fixture.CreateDbContext();

            _dbContext.Providers.AddRange(new List<Provider>
            {
                new Provider { Name = "n1", PolicyNumber = "pn1", GroupNumber = "gn1" },
                new Provider { Name = "n2", PolicyNumber = "pn2", GroupNumber = "gn2" },
                new Provider { Name = "n3", PolicyNumber = "pn3", GroupNumber = "gn3" },
                new Provider { Name = "n4", PolicyNumber = "pn4", GroupNumber = "gn4" },
                new Provider { Name = "n5", PolicyNumber = "pn5", GroupNumber = "gn5" },
                new Provider { Name = "n6", PolicyNumber = "pn6", GroupNumber = "gn6", IsDeleted = true }
            });

            _dbContext.SaveChanges();

            _providerMapper = new ProviderMapper();

            _sut = new ProviderService(_dbContext, _providerMapper);
        }

        [Fact]
        public void GetAllByIds_WithNoIdList_ShouldNotBeEmpty()
        {
            var providers = _sut.GetByIds();
            Assert.NotEmpty(providers);
        }

        [Fact]
        public void GetAllByIds_WithDeletedId_ShouldBeEmpty()
        {
            var deleted = _dbContext.Providers.First(p => p.IsDeleted);
            var providers = _sut.GetByIds(deleted.Id);
            Assert.Empty(providers);
        }

        [Fact]
        public void GetAllByIds_WithId_ShouldReturnProvider()
        {
            var provider = _dbContext.Providers.First(p => !p.IsDeleted);
            var providers = _sut.GetByIds(provider.Id);
            Assert.NotEmpty(providers);
            Assert.Single(providers);
            Assert.Equal(provider.Id, providers.Single().Id.Value);
        }

        [Fact]
        public async void Upsert_WithNewProvider_ShouldInsert()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);

            var sut = new ProviderService(dbContext, _providerMapper);

            var providerModel = new ProviderModel { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };

            var newProvider = await sut.Upsert(providerModel);

            Assert.NotNull(newProvider);
            Assert.NotNull(newProvider.Id);

            var providers = sut.GetByIds(newProvider.Id.Value);
            Assert.NotEmpty(providers);
            Assert.Equal(newProvider.Id.Value, providers.First().Id);
        }

        [Fact]
        public async void Upsert_WithExistingProvider_ShouldUpdate()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ProviderService(dbContext, _providerMapper);

            var existingProvider = new Provider { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };
            dbContext.Providers.Add(existingProvider);
            dbContext.SaveChanges();

            var existingProviderModel = _providerMapper.MapProviderModel(existingProvider);
            existingProviderModel.Name = "newN";
            existingProviderModel.PolicyNumber = "newPN";
            existingProviderModel.GroupNumber = "newGN";

            var updatedProviderModel = await sut.Upsert(existingProviderModel);

            Assert.NotNull(updatedProviderModel);
            Assert.NotNull(updatedProviderModel.Id);
            Assert.Equal(existingProviderModel.Id, updatedProviderModel.Id);
            Assert.Equal(existingProviderModel.Name, updatedProviderModel.Name);
            Assert.Equal(existingProviderModel.PolicyNumber, updatedProviderModel.PolicyNumber);
            Assert.Equal(existingProviderModel.GroupNumber, updatedProviderModel.GroupNumber);
        }

        [Fact]
        public async void DeleteByIds_WithNoIdList_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ProviderService(dbContext, _providerMapper);

            var provider = new Provider { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };
            dbContext.Providers.Add(provider);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds();

            Assert.Empty(deletedIds);

            var providerModels = sut.GetByIds(provider.Id);

            Assert.NotEmpty(providerModels);
            Assert.Equal(provider.Id, providerModels.First().Id.Value);
        }

        [Fact]
        public async void DeleteByIds_WithIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ProviderService(dbContext, _providerMapper);

            var provider1 = new Provider { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };
            var provider2 = new Provider { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };
            dbContext.Providers.AddRange(provider1, provider2);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds(provider1.Id, provider2.Id);

            Assert.NotEmpty(deletedIds);
            Assert.Contains(provider1.Id, deletedIds);
            Assert.Contains(provider2.Id, deletedIds);

            var providerModels = sut.GetByIds(provider1.Id, provider2.Id);

            Assert.Empty(providerModels);
        }
    }
}
