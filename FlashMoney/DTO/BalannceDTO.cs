using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class BalannceDTO
    {
        public string Balance { get; set; }
        public List<ActivityDTO> activities { get; set; }
    }
}
