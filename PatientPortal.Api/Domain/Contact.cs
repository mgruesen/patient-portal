using System;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Domain
{
    public partial class Contact
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual string StreetName { get; set; }
        public virtual string StreetNumber { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool IsMobilePhone { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
