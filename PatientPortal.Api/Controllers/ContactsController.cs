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
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactService _contactService;

        public ContactsController(ILogger<ContactsController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpPost]
        [Route("list")]
        public ReturnModel ListContacts(IdListModel model)
        {
            IEnumerable<object> returnData;
            try
            {
                returnData = _contactService.GetByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = returnData.ToArray() };
        }

        [HttpPost]
        public async Task<ReturnModel> UpsertContact(ContactModel model)
        {
            ContactModel data;
            try
            {
                data = await _contactService.Upsert(model);
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
        public async Task<ReturnModel> DeleteContacts(IdListModel model)
        {
            IEnumerable<Guid> data;
            try
            {
                data = await _contactService.DeleteByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { new IdListModel { Ids = data.ToArray() } } };
        }
    }
}
