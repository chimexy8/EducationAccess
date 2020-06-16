using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class HistoryStatandTrans
    {
        public string status { get; set; }

        public TransactionHistoryModel Transaction { get; set; }
    }
}
