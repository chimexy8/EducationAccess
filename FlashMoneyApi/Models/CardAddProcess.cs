using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class CardAddProcess
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsProcessed { get; set; }
        public bool Status { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public string Phone { get; set; }

        public string CardId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
