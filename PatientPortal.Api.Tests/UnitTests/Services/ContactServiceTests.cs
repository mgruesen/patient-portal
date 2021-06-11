using PatientPortal.Api.Services;
using Xunit;
using PatientPortal.Api.Mappers;
using System;
using PatientPortal.Api.Domain;
using System.Linq;
using System.Collections.Generic;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Tests.UnitTests.Services
{
    public class ContactServiceTests : TestDatabaseFixture
    {
        private readonly IContactMapper _contactMapper;
        private readonly List<Contact> _contacts = new List<Contact>
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
        };

        public ContactServiceTests(TestDatabaseContext dbContext)
            : base(dbContext)
        {
            _contactMapper = new ContactMapper();
        }

        [Fact]
        public void GetAllByIds_WithNoIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            dbContext.Contacts.AddRange(_contacts);
            dbContext.SaveChanges();

            var contacts = sut.GetByIds();
            Assert.NotEmpty(contacts);
        }

        [Fact]
        public void GetAllByIds_WithDeletedId_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            dbContext.Contacts.AddRange(_contacts);
            dbContext.SaveChanges();

            var deleted = dbContext.Contacts.First(p => p.IsDeleted);
            var contacts = sut.GetByIds(deleted.Id);
            Assert.Empty(contacts);
        }

        [Fact]
        public void GetAllByIds_WithId_ShouldReturnContact()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            dbContext.Contacts.AddRange(_contacts);
            dbContext.SaveChanges();

            var contact = dbContext.Contacts.First(p => !p.IsDeleted);
            var contacts = sut.GetByIds(contact.Id);
            Assert.NotEmpty(contacts);
            Assert.Single(contacts);
            Assert.Equal(contact.Id, contacts.Single().Id.Value);
        }

        [Fact]
        public async void Upsert_WithNewContact_ShouldInsert()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);

            var sut = new ContactService(dbContext, _contactMapper);

            var contactModel = new ContactModel
            {
                StreetName = "sname",
                StreetNumber = "snum",
                City = "city",
                State = "state",
                Zipcode = 1,
                EmailAddress = "em",
                PhoneNumber = "pn"
            };

            var newContact = await sut.Upsert(contactModel);

            Assert.NotNull(newContact);
            Assert.NotNull(newContact.Id);

            var contacts = sut.GetByIds(newContact.Id.Value);
            Assert.NotEmpty(contacts);
            Assert.Equal(newContact.Id.Value, contacts.First().Id);
        }

        [Fact]
        public async void Upsert_WithExistingContact_ShouldUpdate()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            var existingContact = new Contact
            {
                StreetName = "sname",
                StreetNumber = "snum",
                City = "city",
                State = "state",
                Zipcode = 1,
                EmailAddress = "em",
                PhoneNumber = "pn"
            };
            dbContext.Contacts.Add(existingContact);
            dbContext.SaveChanges();

            var existingContactModel = _contactMapper.MapContactModel(existingContact);
            existingContactModel.StreetName = "newSName";
            existingContactModel.StreetNumber = "newSNum";
            existingContactModel.City = "newC";
            existingContactModel.State = "newS";
            existingContactModel.Zipcode = 11;
            existingContactModel.PhoneNumber = "newPN";
            existingContactModel.IsMobilePhone = true;
            existingContactModel.EmailAddress = "newE";

            var updatedContactModel = await sut.Upsert(existingContactModel);

            Assert.NotNull(updatedContactModel);
            Assert.NotNull(updatedContactModel.Id);
            Assert.Equal(existingContactModel.Id, updatedContactModel.Id);
            Assert.Equal(existingContactModel.StreetName, updatedContactModel.StreetName);
            Assert.Equal(existingContactModel.StreetNumber, updatedContactModel.StreetNumber);
            Assert.Equal(existingContactModel.City, updatedContactModel.City);
            Assert.Equal(existingContactModel.State, updatedContactModel.State);
            Assert.Equal(existingContactModel.Zipcode, updatedContactModel.Zipcode);
            Assert.Equal(existingContactModel.PhoneNumber, updatedContactModel.PhoneNumber);
            Assert.Equal(existingContactModel.EmailAddress, updatedContactModel.EmailAddress);
            Assert.Equal(existingContactModel.IsMobilePhone, updatedContactModel.IsMobilePhone);
        }

        [Fact]
        public async void DeleteByIds_WithNoIdList_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            var contact = new Contact
            {
                StreetName = "sname",
                StreetNumber = "snum",
                City = "city",
                State = "state",
                Zipcode = 1,
                EmailAddress = "em",
                PhoneNumber = "pn"
            };
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds();

            Assert.Empty(deletedIds);

            var contactModels = sut.GetByIds(contact.Id);

            Assert.NotEmpty(contactModels);
            Assert.Equal(contact.Id, contactModels.First().Id.Value);
        }

        [Fact]
        public async void DeleteByIds_WithIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new ContactService(dbContext, _contactMapper);

            var contact1 = new Contact
            {
                StreetName = "sname1",
                StreetNumber = "snum1",
                City = "city1",
                State = "state1",
                Zipcode = 1,
                EmailAddress = "em1",
                PhoneNumber = "pn1"
            };
            var contact2 = new Contact
            {
                StreetName = "sname2",
                StreetNumber = "snum2",
                City = "city2",
                State = "state2",
                Zipcode = 2,
                EmailAddress = "em2",
                PhoneNumber = "pn2"
            };
            dbContext.Contacts.AddRange(contact1, contact2);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds(contact1.Id);

            Assert.NotEmpty(deletedIds);
            Assert.Contains(contact1.Id, deletedIds);
            Assert.DoesNotContain(contact2.Id, deletedIds);

            var contactModels = sut.GetByIds(contact1.Id, contact2.Id);

            Assert.NotEmpty(contactModels);
            Assert.Contains(contact2.Id, contactModels.Select(c => c.Id.Value));
            Assert.DoesNotContain(contact1.Id, contactModels.Select(c => c.Id.Value));
        }
    }
}
