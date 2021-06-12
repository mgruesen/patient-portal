using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string IsMobilePhone { get; set; }
        public string ProviderName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
    }
}
