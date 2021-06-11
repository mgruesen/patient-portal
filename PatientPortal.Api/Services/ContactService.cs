using System;
using System.Collections.Generic;
using PatientPortal.Api;
using PatientPortal.Api.Models;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PatientPortal.Api.Services
{
    public interface IContactService
    {
        IEnumerable<ContactModel> GetByIds(Guid[] contactIds);
        Task<ContactModel> Upsert(ContactModel contact);
        Task<IEnumerable<Guid>> DeleteByIds(Guid[] contactIds);
    }

    public class ContactService : IContactService
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IContactMapper _contactMapper;

        public ContactService(PatientPortalDbContext dbContext, IContactMapper contactMapper)
        {
            _dbContext = dbContext;
            _contactMapper = contactMapper;
        }

        public IEnumerable<ContactModel> GetByIds(params Guid[] contactIds)
        {
            var contacts = contactIds.Length == 0 ?
                _dbContext.Contacts.Where(c => !c.IsDeleted) :
                _dbContext.Contacts.Where(c =>
                    contactIds.Contains(c.Id) && !c.IsDeleted);

            return contacts.Select(_contactMapper.MapContactModel);
        }

        public async Task<ContactModel> Upsert(ContactModel contactModel)
        {
            var contact = _dbContext.Contacts
                .SingleOrDefault(c => c.Id == contactModel.Id && !c.IsDeleted);

            if (contact == null)
            {
                contact = _contactMapper.MapContact(contactModel);
                await _dbContext.Contacts.AddAsync(contact);
            }
            else
            {
                contact = _contactMapper.MapContact(contactModel);
            }

            await _dbContext.SaveChangesAsync();

            return _contactMapper.MapContactModel(contact);
        }

        public async Task<IEnumerable<Guid>> DeleteByIds(params Guid[] contactIds)
        {
            var contacts = _dbContext.Contacts
                .Where(c => contactIds.Contains(c.Id) && !c.IsDeleted);

            var deletedIds = contacts.Select(c => c.Id).ToList();

            foreach (var contact in contacts)
                contact.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return deletedIds;
        }
    }
}
