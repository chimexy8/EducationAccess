using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class NotificationDTO
    {
        public bool AllowTransactionNotif { get; set; }
        public bool AllowActivityNotif { get; set; }
    }
}
