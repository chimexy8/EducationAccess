using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class ClaimDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }
}
