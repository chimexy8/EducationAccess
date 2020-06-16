using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class ResetPinDTO
    {

        public string Phone { get; set; }
        public string OldPin { get; set; }
        public string NewPin { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Current PIN 1")]
        public string OldPin1 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current PIN 2")]
        public string OldPin2 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current PIN 3")]
        public string OldPin3 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current PIN 4")]
        public string OldPin4 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "New PIN 1")]
        public string NewPin1 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Transaction PIN 2")]
        public string NewPin2 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Transaction PIN 3")]
        public string NewPin3 { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Transaction PIN 4")]
        public string NewPin4 { get; set; }

        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Cinfirmation PIN 1")]
        [Compare("NewPin1")]
        public string ConfirmNewPin1 { get; set; }
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Cinfirmation PIN 2")]
        [Compare("NewPin2")]
        public string ConfirmNewPin2 { get; set; }
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Cinfirmation PIN 3")]
        [Compare("NewPin3")]
        public string ConfirmNewPin3 { get; set; }
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Cinfirmation PIN 4")]
        [Compare("NewPin4")]
        public string ConfirmNewPin4 { get; set; }
    }
}
