using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Mappers
{
    public interface IContactMapper
    {
        Contact MapContact(ContactModel contactModel);
        ContactModel MapContactModel(Contact contact);
    }

    public class ContactMapper : IContactMapper
    {
        public Contact MapContact(ContactModel contactModel)
        {
            return new()
            {
                StreetName = contactModel.StreetName,
                StreetNumber = contactModel.StreetNumber,
                City = contactModel.City,
                State = contactModel.State,
                Zipcode = contactModel.Zipcode,
                EmailAddress = contactModel.EmailAddress,
                PhoneNumber = contactModel.PhoneNumber,
                IsMobilePhone = contactModel.IsMobilePhone,
            };
        }

        public ContactModel MapContactModel(Contact contact)
        {
            return new()
            {
                Id = contact.Id,
                StreetName = contact.StreetName,
                StreetNumber = contact.StreetNumber,
                City = contact.City,
                State = contact.State,
                Zipcode = contact.Zipcode,
                EmailAddress = contact.EmailAddress,
                PhoneNumber = contact.PhoneNumber,
                IsMobilePhone = contact.IsMobilePhone,
            };
        }
    }
}
