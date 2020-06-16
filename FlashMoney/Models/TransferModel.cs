using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class TransferModel
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderPhone { get; set; }
        public Guid? ReceiverId { get; set; }
        public string ReceiverPhone { get; set; }
        public string Code { get; set; }
        public DateTime SendDate { get; set; }
        public decimal Amount { get; set; }
        public bool Claimed { get; set; }
        public string Narration { get; set; }
        public string Url { get; set; }
    }
}
