using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class CardDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string UserEmail { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CardUrl { get; set; }
        public string CardRef { get; set; }
        public string Token { get; set; }
        public string CardPIN { get; set; }
        public string TransID { get; set; }
        public string CardMessage { get; set; }
        public string CVV { get; internal set; }
        public string Amount { get; set; }
        public string CardType { get; set; }
        public bool Valid { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public decimal LastDebited { get; set; }
        public int TransactionCount { get; set; }
    }
}
