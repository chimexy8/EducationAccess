using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class PaymentModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DataModel data { get; set; }
    }
}
