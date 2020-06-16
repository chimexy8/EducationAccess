using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class AddCardModel
    {
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public decimal Amount { get; set; }

        [StringLength(19)]
        [Required]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public string ExpiryMonth { get; set; }

        [Required]
        public string ExpiryYear { get; set; }

        [Required]
        [StringLength(4)]
        [DataType(DataType.Password)]
        public string Pin { get; set; }

        [Required]
        [Display(Name = "Card Holder")]
        public string CardHolder { get; set; }
    }
}
