using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Mappers;
using PatientPortal.Api.Models;
using Xunit;

namespace PatientPortal.Api.Tests.UnitTests.Mappers
{
    public class ContactMapperTests
    {
        private readonly IContactMapper _mut;

        public ContactMapperTests()
        {
            _mut = new ContactMapper();
        }

        [Fact]
        public void MapContact_ShouldMapContact()
        {
            var model = new ContactModel
            {
                Id = Guid.NewGuid(),
                StreetName = "sname",
                StreetNumber = "snum",
                City = "city",
                State = "state",
                Zipcode = "1",
                EmailAddress = "em",
                PhoneNumber = "pn",
                IsMobilePhone = true
            };

            var contact = _mut.MapContact(model);

            Assert.Equal(model.Id, contact.Id);
            Assert.Equal(model.StreetName, contact.StreetName);
            Assert.Equal(model.StreetNumber, contact.StreetNumber);
            Assert.Equal(model.City, contact.City);
            Assert.Equal(model.State, contact.State);
            Assert.Equal(model.Zipcode, contact.Zipcode);
            Assert.Equal(model.EmailAddress, contact.EmailAddress);
            Assert.Equal(model.PhoneNumber, contact.PhoneNumber);
            Assert.Equal(model.IsMobilePhone, contact.IsMobilePhone);
        }

        [Fact]
        public void MapContactModel_ShouldMapContactModel()
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                StreetName = "sname",
                StreetNumber = "snum",
                City = "city",
                State = "state",
                Zipcode = "1",
                EmailAddress = "em",
                PhoneNumber = "pn",
                IsMobilePhone = true
            };

            var model = _mut.MapContactModel(contact);

            Assert.Equal(contact.Id, model.Id);
            Assert.Equal(contact.StreetName, model.StreetName);
            Assert.Equal(contact.StreetNumber, model.StreetNumber);
            Assert.Equal(contact.City, model.City);
            Assert.Equal(contact.State, model.State);
            Assert.Equal(contact.Zipcode, model.Zipcode);
            Assert.Equal(contact.EmailAddress, model.EmailAddress);
            Assert.Equal(contact.PhoneNumber, model.PhoneNumber);
            Assert.Equal(contact.IsMobilePhone, model.IsMobilePhone);
        }
    }
}
