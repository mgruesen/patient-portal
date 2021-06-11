using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Models
{
    public class ProviderModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PolicyNumber { get; set; }
        [Required]
        public string GroupNumber { get; set; }
    }
}
