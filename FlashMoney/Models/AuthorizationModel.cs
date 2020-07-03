using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class AuthorizationModel
    {
        public string authorization_code { get; set; }
        public string card_type { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string bin { get; set; }
        public string bank { get; set; }
        public string channel { get; set; }
        public string reuseable { get; set; }
        public string country_code { get; set; }
    }
}
