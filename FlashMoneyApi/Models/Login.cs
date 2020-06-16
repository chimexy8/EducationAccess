using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        //public string ConnectionId { get; set; }
        public string SessionId { get; set; }
        public bool LoggedIn { get; set; }
    }
}
