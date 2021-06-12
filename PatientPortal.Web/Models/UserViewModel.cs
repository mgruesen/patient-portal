using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsMobilePhone { get; set; }
        public string ProviderName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
    }
}
