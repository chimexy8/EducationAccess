using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class Create2FADTO
    {
        public string Phone { get; set; }
        public string TwoFA { get; set; }
        public string NewTwoFA { get; set; }
    }
}
