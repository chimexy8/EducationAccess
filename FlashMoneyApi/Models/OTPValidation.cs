using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class OTPValidation
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
    }
}
