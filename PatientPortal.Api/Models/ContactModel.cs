using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Models
{
    public class ContactModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsMobilePhone { get; set; }
    }
}
