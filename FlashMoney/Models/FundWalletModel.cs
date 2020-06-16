using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class FundWalletModel
    {

        [Required]
        public string Phone { get; set; }

        [Required]
        public decimal Amount { get; set; }

        
    }
}
