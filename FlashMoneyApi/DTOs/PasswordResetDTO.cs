using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class PasswordResetDTO
    {
        public string Status { get; set; }
        public string Rcode { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
