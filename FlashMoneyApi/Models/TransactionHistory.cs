using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class TransactionHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HistOwnerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }
        public Status Status { get; set; }
        public string Phone { get; set; }
        public string CardId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CardType { get; set; }
        public string Receipient { get; set; }
        public string ReceipientPassport { get; set; }
        public string DestinationPhone { get; set; }
    }
    public enum Status
    {
        Completed,Waiting
    }
}
