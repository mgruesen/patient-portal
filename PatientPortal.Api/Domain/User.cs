using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientPortal.Api.Domain
{
    public class User
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Key]
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual Guid? PatientId { get; set; }

#nullable enable
        [ForeignKey("PatientId")]
        public virtual Patient? Patient { get; set; }
#nullable disable
    }
}
