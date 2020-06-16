using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class WithdrawDTO
    {
        public string Phone { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public int Pin { get; set; }
        public string Naration { get; set; }

    }
}
