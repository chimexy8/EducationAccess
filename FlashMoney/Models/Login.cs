﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class Login
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string ConnectionId { get; set; }
        public bool LoggedIn { get; set; }


    }
}
