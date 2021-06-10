using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Mappers;
using PatientPortal.Api.Models;
using Xunit;

namespace PatientPortal.Api.Tests.UnitTests.Mappers
{
    public class ProviderMapperTests
    {
        private readonly IProviderMapper _mut;

        public ProviderMapperTests()
        {
            _mut = new ProviderMapper();
        }

        [Fact]
        public void MapProvider_ShouldMapProvider()
        {
            var model = new ProviderModel
            {
                Id = Guid.NewGuid(),
                Name = "name",
                PolicyNumber = "policyNumber",
                GroupNumber = "groupNumber"
            };

            var provider = _mut.MapProvider(model);

            Assert.Equal(model.Id, provider.Id);
            Assert.Equal(model.Name, provider.Name);
            Assert.Equal(model.PolicyNumber, provider.PolicyNumber);
            Assert.Equal(model.GroupNumber, provider.GroupNumber);
        }

        [Fact]
        public void MapProviderModel_ShouldMapProviderModel()
        {
            var provider = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "name",
                PolicyNumber = "policyNumber",
                GroupNumber = "groupNumber"
            };

            var model = _mut.MapProviderModel(provider);

            Assert.Equal(provider.Id, model.Id);
            Assert.Equal(provider.Name, model.Name);
            Assert.Equal(provider.PolicyNumber, model.PolicyNumber);
            Assert.Equal(provider.GroupNumber, model.GroupNumber);
        }
    }
}
