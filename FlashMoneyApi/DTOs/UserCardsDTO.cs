using FlashMoneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class UserCardsDTO
    {
        public decimal Amount { get; set; }
        public List<CardDetail> Cards { get; set; } = new List<CardDetail>();
    }
    public class AddCardReferenceDTO
    {
        public decimal Amount { get; set; }
        public string SourcePhone { get; set; }
        public string Reference { get; set; }
    }
}
