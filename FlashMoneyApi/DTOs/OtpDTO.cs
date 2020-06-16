using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class OtpDTO
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Otp { get; set; }
    }
}
