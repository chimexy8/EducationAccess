using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class NotificationDTO
    {
        public bool AllowTransactionNotif { get; set; }
        public bool AllowActivityNotif { get; set; }
        public string Phone { get; set; }
    }
}
