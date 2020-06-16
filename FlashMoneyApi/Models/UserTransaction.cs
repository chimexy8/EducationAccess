using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class UserTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string TransactionReference { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public bool IsAddCard { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CVV { get; set; }
        public string CardType { get; set; }
        public User User { get; set; }
    }
    public enum TransactionType
    {
        Debit, Credit
    }

    public enum TransactionStatus
    {
        UnSuccessful, Successful, Attempted
    }
}
