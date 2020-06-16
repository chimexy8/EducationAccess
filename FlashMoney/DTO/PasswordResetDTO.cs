using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class PasswordResetDTO
    {
        public string Status { get; set; }
        public string Rcode { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
