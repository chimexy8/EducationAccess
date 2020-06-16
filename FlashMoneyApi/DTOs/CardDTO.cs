using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public string CardNumberFux { get; set; }
        public string expire { get; set; }
        public string CVV { get; set; }
        public string SourcePhone { get; set; }
        public string Pin { get; set; }
    }
}
