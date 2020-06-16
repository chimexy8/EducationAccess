using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class ResetPinDTO
    {
        public string OldPin { get; set; }
        public string NewPin { get; set; }
        public string Phone { get; set; }
    }
}
