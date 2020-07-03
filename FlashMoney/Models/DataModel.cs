using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class DataModel
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string transaction_Date { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public string domain { get; set; }

        //[JsonProperty("metadata")]
        //public string metadata { get; set; }
        public string gateway_response { get; set; }
        public string message { get; set; }
        public string channel { get; set; }
        public string ip_address { get; set; }
        public LogModel log { get; set; }
        public string fees { get; set; }
        public AuthorizationModel authorization { get; set; }
        public CustomerModel customer { get; set; }
        public string plane { get; set; }
    }
}
