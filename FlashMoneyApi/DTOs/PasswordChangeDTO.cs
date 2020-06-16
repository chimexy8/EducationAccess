using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class PasswordChangeDTO
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string Phone { get; set; }
        public string RCode { get; set; }
    }
}
