using System;

namespace PatientPortal.Api.Models
{
    public class ProviderModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
    }
}
