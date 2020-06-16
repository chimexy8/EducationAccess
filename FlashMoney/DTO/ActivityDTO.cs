using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class ActivityDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }

        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Debit, Credit
    }
}
