using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal CurrentBallance { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
