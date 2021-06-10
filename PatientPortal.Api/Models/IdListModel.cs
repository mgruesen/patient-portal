using System;

namespace PatientPortal.Api.Models
{
    public class IdListModel
    {
        public Guid[] Ids { get; set; } = Array.Empty<Guid>();
    }
}
