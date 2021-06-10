using PatientPortal.Services;
using Xunit;
using PatientPortal.Api.Mappers;
using System;
using PatientPortal.Api.Domain;
using System.Linq;
using System.Collections.Generic;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Tests.Services
{
    public class ContactServiceTests : TestDatabaseFixture
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IContactMapper _contactMapper;
        private readonly ContactService _sut;

        public ContactServiceTests(TestDatabaseContext dbContext)
            : base(dbContext)
        {
            _dbContext = Fixture.CreateDbContext();

            _dbContext.Contacts.AddRange(new List<Contact>
            {
                new Contact
                {
                    StreetName = "sname",
                    StreetNumber = "snum",
                    City = "city",
                    State = "state",
                    Zipcode = 1,
                    EmailAddress = "em",
                    PhoneNumber = "pn"
                },
                new Contact
                {
                    StreetName = "sname2",
                    StreetNumber = "snum2",
                    City = "city2",
                    State = "state2",
                    Zipcode = 2,
                    EmailAddress = "em2",
                    PhoneNumber = "pn2"
                },new Contact
                {
                    StreetName = "sname3",
                    StreetNumber = "snum3",
                    City = "city3",
                    State = "state3",
                    Zipcode = 3,
                    EmailAddress = "em3",
                    PhoneNumber = "pn3",
                    IsDeleted = true
                },
            });

            _dbContext.SaveChanges();

            _contactMapper = new ContactMapper();

            _sut = new ContactService(_dbContext, _contactMapper);
        }

        [Fact]
        public void GetAllByIds_WithNoIdList_ShouldNotBeEmpty()
        {
            var contacts = _sut.GetByIds();
            Assert.NotEmpty(contacts);
        }

        [Fact]
        public void GetAllByIds_WithDeletedId_ShouldBeEmpty()
        {
            var deleted = _dbContext.Contacts.First(p => p.IsDeleted);
            var contacts = _sut.GetByIds(deleted.Id);
            Assert.Empty(contacts);
        }

        [Fact]
        public void GetAllByIds_WithId_ShouldReturnProvider()
        {
            var contact = _dbContext.Contacts.First(p => !p.IsDeleted);
            var contacts = _sut.GetByIds(contact.Id);
            Assert.NotEmpty(contacts);
            Assert.Single(contacts);
            Assert.Equal(contact.Id, contacts.Single().Id.Value);
        }

        [Fact]
        public async void Upsert_WithNewProvider_ShouldInsert()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);

            var sut = new ContactService(dbContext, _contactMapper);

            var contactModel = new ContactModel
            {
                Name = "n", PolicyNumber = "pn", GroupNumber = "gn"
            };

            var newContact = await sut.Upsert(contactModel);

            Assert.NotNull(newContact);
            Assert.NotNull(newContact.Id);

            var contacts = sut.GetByIds(newContact.Id.Value);
            Assert.NotEmpty(contacts);
            Assert.Equal(newContact.Id.Value, contacts.First().Id);
        }

        [Fact]
        public async void Upsert_WithExistingProvider_ShouldUpdate()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            var existingContact = new Contact { Name = "n", PolicyNumber = "pn", GroupNumber = "gn" };
            dbContext.Providers.Add(existingContact);
            dbContext.SaveChanges();

            var existingContactModel = _contactMapper.MapContactModel(existingContact);
            existingContactModel.StreetName = "newN";
            existingContactModel.StreetNumber = "newPN";
            existingContactModel.City = "newGN";

            var updatedContactModel = await sut.Upsert(existingContactModel);

            Assert.NotNull(updatedContactModel);
            Assert.NotNull(updatedContactModel.Id);
            Assert.Equal(existingContactModel.Id, updatedContactModel.Id);
            Assert.Equal(existingContactModel.Name, updatedContactModel.Name);
            Assert.Equal(existingContactModel.PolicyNumber, updatedContactModel.PolicyNumber);
            Assert.Equal(existingContactModel.GroupNumber, updatedContactModel.GroupNumber);
        }

        [Fact]
        public async void DeleteByIds_WithNoIdList_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

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
            var sut = new ContactService(dbContext, _contactMapper);

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
