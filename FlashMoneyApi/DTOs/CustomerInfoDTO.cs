using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class CustomerInfoDTO
    {
        public Data auth { get; set; }
        public string requestId { get; set; }
        public string mobileNo { get; set; }
    }
    public class Data
    {
        public string clientApiKeyField { get; set; }
        public string clientIdField { get; set; }
        public string mACField { get; set; }
    } 
}
