using System;
using System.Collections.Generic;

namespace PatientPortal.Api.Models
{
    public class ReturnModel
    {
        public string Status => Error == null ? "success" : "error";
        public object[] Data { get; set; }
        public object Error { get; set; }
    }
}
