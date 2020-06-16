using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class TwoFADTO
    {

        public string Phone { get; set; }

        public bool HasTransactionPin { get; set; }
        public bool HasAuthPin { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Transaction PIN")]
        public string TwoFA { get; set; }
        public string TwoFA1 { get; set; }
        public string TwoFA2 { get; set; }
        public string TwoFA3 { get; set; }
        public string TwoFA4 { get; set; }


        //[Required]
        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name ="New Transaction PIN")]
        public string NewTwoFA { get; set; }
        public string NewTwoFA1 { get; set; }
        public string NewTwoFA2 { get; set; }
        public string NewTwoFA3 { get; set; }
        public string NewTwoFA4 { get; set; }

        [DataType(DataType.Password)]
        //[StringLength(4)]
        [Display(Name = "Confirm Transaction PIN")]
        //[Compare("NewTwoFA")]
        public string ConfirmNewTwoFA { get; set; }
        public string ConfirmNewTwoFA1 { get; set; }
        public string ConfirmNewTwoFA2 { get; set; }
        public string ConfirmNewTwoFA3 { get; set; }
        public string ConfirmNewTwoFA4 { get; set; }
    }
}
