using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class ClaimDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }
}
