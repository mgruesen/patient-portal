using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Models
{
    public class PatientModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
