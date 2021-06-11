using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Models
{
    public class UserModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public Guid? PatientId { get; set; }
    }
}
