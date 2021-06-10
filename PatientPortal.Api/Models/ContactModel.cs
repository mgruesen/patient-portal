using System;

namespace PatientPortal.Api.Models
{
    public class ContactModel
    {
        public Guid? Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMobilePhone { get; set; }
    }
}
