using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Models
{
    public class UserModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProviderId { get; set; }
    }
}
