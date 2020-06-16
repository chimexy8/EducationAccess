using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class PhoneBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
           return connection.User?.Claims.FirstOrDefault(p => p.Type == "phone")?.Value;
            //return connection.User?.FindFirst(ClaimTypes.Phone)?.Value;
        }
    }
}
