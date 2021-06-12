using System;
using System.Collections.Generic;

namespace PatientPortal.Api.Models
{
    public class ResponseModel
    {
        public string Status => Error == null ? "success" : "error";
        public object[] Data { get; set; }
        public object Error { get; set; }
    }
}
