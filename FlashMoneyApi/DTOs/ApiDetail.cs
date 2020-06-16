using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class ApiDetail
    {
        public string ClientId { get; set; }
        public string ClientAPIKey { get; set; }
        public string ClientSecretKey { get; set; }
        public string ClientIVKey { get; set; }
    }
}
