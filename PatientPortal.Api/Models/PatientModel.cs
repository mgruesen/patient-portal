using System;
using System.ComponentModel.DataAnnotations;
using PatientPortal.Api.Domain;

namespace PatientPortal.Api.Models
{
    public class PatientModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProviderId { get; set; }
    }
}
