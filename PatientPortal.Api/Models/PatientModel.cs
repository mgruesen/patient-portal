using System;

namespace PatientPortal.Api.Models
{
    public class PatientModel
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
