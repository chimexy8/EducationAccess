using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class MultiPinTransferModel
    {
        [Required]
        [Display(Name = "Source Phone")]
        public string SourcePhone { get; set; }

        public string Rps { get; set; }

        public string AuthType { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Pin { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Tap { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Otp { get; set; }

        public decimal TransferCharge { get; set; }
    }
}
