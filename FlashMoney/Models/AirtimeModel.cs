using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class AirtimeModel
    {
        [Required]
        [Display(Name = "Phone")]
        public string SourcePhone { get; set; }

        [Required]
        [Display(Name = "Destination Phone")]
        public string DestinationPhone { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public NetWork NetWork { get; set; }

        [Required]
        public int Pin { get; set; }
    }

    public enum NetWork
    {
        MTN, Glo, [Display(Name = "9Mobile")] Etisalat, Airtel
    }
}
