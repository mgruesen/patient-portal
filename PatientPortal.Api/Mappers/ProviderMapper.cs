using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Mappers
{
    public interface IProviderMapper
    {
        Provider MapProvider(ProviderModel providerModel);
        ProviderModel MapProviderModel(Provider provider);
    }

    public class ProviderMapper : IProviderMapper
    {
        public Provider MapProvider(ProviderModel provider)
        {
            return new()
            {
                Id = provider.Id ?? Guid.Empty,
                Name = provider.Name,
                PolicyNumber = provider.PolicyNumber,
                GroupNumber = provider.GroupNumber,
            };
        }

        public ProviderModel MapProviderModel(Provider provider)
        {
            return new()
            {
                Id = provider.Id,
                Name = provider.Name,
                PolicyNumber = provider.PolicyNumber,
                GroupNumber = provider.GroupNumber,
            };
        }
    }
}
