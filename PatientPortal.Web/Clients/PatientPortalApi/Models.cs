using System;
using System.Collections.Generic;

namespace PatientPortal.Web.Clients.PatientPortalApi
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class IdListModel
    {
        public Guid[] Ids { get; set; } = Array.Empty<Guid>();
    }

    public class ContactModel
    {
        public Guid Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMobilePhone { get; set; }
    }

    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProviderId { get; set; }
    }

    public class ProviderModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
    }

    public class PatientModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ResponseModel<T>
    {
        public string Status { get; set; }
        public T[] Data { get; set; }
        public object Error { get; set; }
    }
}
