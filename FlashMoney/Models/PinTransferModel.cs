using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class PinTransferModel
    {
        [Required]
        [Display(Name = "Source Phone")]
        public string SourcePhone { get; set; }

        [Required]
        [Display(Name = "Destination Phone")]
        public string DestinationPhone { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Narration { get; set; }

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

        public Guid CardId { get; set; }
    }
}
