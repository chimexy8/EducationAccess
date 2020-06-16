using FlashMoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class TransInfo
    {
        public string status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public List<MultipleTranResp> Resp { get; set; }
    }
}
