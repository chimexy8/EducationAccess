using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class ResetPinModel
    {
        [Required]
        [Display(Name = "Old Pin")]
        public string OldPin { get; set; }
        [Required]
        [Display(Name = "New Pin")]
        public string NewPin { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
