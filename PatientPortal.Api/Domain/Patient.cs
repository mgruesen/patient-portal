using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientPortal.Api.Domain
{

    public partial class Patient
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Guid? ContactId { get; set; }
        public virtual Guid? ProviderId { get; set; }
        public virtual bool IsDeleted { get; set; }

#nullable enable
        [ForeignKey("ContactId")]
        public virtual Contact? Contact { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider? Provider { get; set; }
#nullable disable
    }
}
