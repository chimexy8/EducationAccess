using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlashMoney.Hubs
{
    public  class ChatHub : Hub
    {
        public async Task SendMessage(string user/*, string message*/)
        {
            var message = $"Send message to you with user id {user}";
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

     
    }
}
