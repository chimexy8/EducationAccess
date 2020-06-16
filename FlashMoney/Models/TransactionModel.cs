using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class TransactionModel
    {
        [Display(Name = "Source Phone")]
        public string SourcePhone { get; set; }

        [Required]
        [Display(Name = "Destination Phone")]
        public string DestinationPhone { get; set; }

        public string Receipient { get; set; }

        public decimal TransacrtionCharge { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        public string Narration { get; set; }
        
        public string AuthType { get; set; }

        public string Pin { get; set; }


        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }

        public string PinOne { get; set; }
        public string PinTwo { get; set; }
        public string PinThree { get; set; }
        public string PinFour { get; set; }
    }
}
