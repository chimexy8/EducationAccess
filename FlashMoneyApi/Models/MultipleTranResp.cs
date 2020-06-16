using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class MultipleTranResp
    {
        public string Phone { get; set; }
        public string Response { get; set; }
        public decimal Amount { get; set; }
    }
}
