using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientPortal.Api.Models;
using PatientPortal.Api.Services;

namespace PatientPortal.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IProviderService _providerService;

        public ProvidersController(ILogger<ProvidersController> logger, IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        [HttpPost]
        [Route("list")]
        public ResponseModel ListProviders(IdListModel model)
        {
            IEnumerable<object> returnData;
            try
            {
                returnData = _providerService.GetByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = returnData.ToArray() };
        }

        [HttpPost]
        public async Task<ResponseModel> UpsertProvider(ProviderModel model)
        {
            ProviderModel data;
            try
            {
                data = await _providerService.Upsert(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { data } };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ResponseModel> DeleteProviders(IdListModel model)
        {
            IEnumerable<Guid> data;
            try
            {
                data = await _providerService.DeleteByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { new { DeletedIds = data.ToArray() } } };
        }
    }
}
