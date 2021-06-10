using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Domain
{
    public partial class Provider
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string PolicyNumber { get; set; }
        public virtual string GroupNumber { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
