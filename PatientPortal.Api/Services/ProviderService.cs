using System;
using System.Collections.Generic;
using PatientPortal.Api;
using PatientPortal.Api.Models;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PatientPortal.Services
{
    public interface IProviderService
    {
        IEnumerable<ProviderModel> GetByIds(Guid[] providerIds);
        Task<ProviderModel> Upsert(ProviderModel provider);
        Task<IEnumerable<Guid>> DeleteByIds(Guid[] providerIds);
    }

    public class ProviderService : IProviderService
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IProviderMapper _providerMapper;

        public ProviderService(PatientPortalDbContext dbContext, IProviderMapper providerMapper)
        {
            _dbContext = dbContext;
            _providerMapper = providerMapper;
        }

        public IEnumerable<ProviderModel> GetByIds(params Guid[] providerIds)
        {
            var providers = providerIds.Length == 0 ?
                _dbContext.Providers.Where(p => !p.IsDeleted) :
                _dbContext.Providers.Where(p =>
                    providerIds.Contains(p.Id) && !p.IsDeleted);

            return providers.Select(_providerMapper.MapProviderModel);
        }

        public async Task<ProviderModel> Upsert(ProviderModel providerModel)
        {
            var provider = _dbContext.Providers
                .SingleOrDefault(p => p.Id == providerModel.Id && !p.IsDeleted);

            if (provider == null)
            {
                provider = _providerMapper.MapProvider(providerModel);
                await _dbContext.Providers.AddAsync(provider);
            }
            else
            {
                provider = _providerMapper.MapProvider(providerModel);
            }

            await _dbContext.SaveChangesAsync();

            return _providerMapper.MapProviderModel(provider);
        }

        public async Task<IEnumerable<Guid>> DeleteByIds(params Guid[] providerIds)
        {
            var providers = _dbContext.Providers
                .Where(p => providerIds.Contains(p.Id) && !p.IsDeleted);

            var deletedIds = providers.Select(p => p.Id).ToList();

            foreach (var provider in providers)
                provider.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return deletedIds;
        }
    }
}
