using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class MultiTransferDTO
    {
        public string SourcePhone { get; set; }
        public string AuthType { get; set; }
        public string Pin { get; set; }
        public string Tap { get; set; }
        public string Otp { get; set; }
        public decimal TransferCharge { get; set; }
        public string Rps { get; set; }
    }
}
