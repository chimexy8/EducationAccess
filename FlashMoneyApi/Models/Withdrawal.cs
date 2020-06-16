using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class Withdrawal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public string RequestId { get; internal set; }
    }
}
