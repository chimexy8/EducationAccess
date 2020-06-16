using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class WithdrawalModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        [Display(Name ="Account Number")]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int Pin { get; set; }
        public string Naration { get; set; }
    }
}
