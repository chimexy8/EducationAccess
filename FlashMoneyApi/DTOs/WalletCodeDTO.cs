using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class WalletCodeDTO
    {
        public string amountField { get; set; }
        public string codeStatusField { get; set; }
        public string codeTypeField { get; set; }
        public string generatedWalletCodeField { get; set; }
        public string requestIdField { get; set; }
        public string statusCodeField { get; set; }
        public string statusDescriptionField { get; set; }
    }
}
