using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class UserCardsDTO
    {
        public decimal Amount { get; set; }
        public decimal Charge { get; set; }
        public List<CardDetail> Cards { get; set; } = new List<CardDetail>();
        public AddCardDTO AddCardDTO { get; set; }
    }

    public class AddCardDTO
    {
        public string CardNumber { get; set; }
        public string CardNumberFux { get; set; }
        public string expire { get; set; }
        public string CVV { get; set; }
        public string SourcePhone { get; set; }
        public string Pin { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
    }

    public class FundWalletDTO
    {
        public decimal Amount { get; set; }
        public string SourcePhone { get; set; }
        public string AuthType { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
    }

    public class AddCardReferenceDTO
    {
        public decimal Amount { get; set; }
        public string SourcePhone { get; set; }
        public string Reference { get; set; }
    }
}
