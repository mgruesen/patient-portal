using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientPortal.Api.Models;
using PatientPortal.Api.Services;

namespace PatientPortal.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _paitentService;

        public PatientsController(ILogger<PatientsController> logger, IPatientService paitentService)
        {
            _logger = logger;
            _paitentService = paitentService;
        }

        [HttpPost]
        [Route("list")]
        public ReturnModel ListPatients(IdListModel model)
        {
            IEnumerable<object> returnData;
            try
            {
                returnData = _paitentService.GetByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = returnData.ToArray() };
        }

        [HttpPost]
        public async Task<ReturnModel> UpsertPatient(PatientModel patientModel)
        {
            PatientModel returnModel;
            try
            {
                returnModel = await _paitentService.Upsert(patientModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { returnModel } };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ReturnModel> DeletePatients(IdListModel model)
        {
            IEnumerable<Guid> returnData;
            try
            {
                returnData = await _paitentService.DeleteByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { new IdListModel { Ids = returnData.ToArray() } } };
        }
    }
}
