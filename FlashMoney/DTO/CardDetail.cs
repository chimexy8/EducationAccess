using System;

namespace FlashMoney.DTO
{
    public class CardDetail
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CardType { get; set; }
        public bool Active { get; set; }
        public decimal LastDebited { get; set; }
        public int TransactionCount { get; set; }


    }
}