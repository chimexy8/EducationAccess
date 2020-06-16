using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class CustumerInfoInput
    {
        public auth auth { get; set; }
        public string requestId { get; set; }
        public string mobileNo { get; set; }
    }

    public class auth
    {
        public string clientApiKeyField { get; set; }
        public string clientIdField { get; set; }
        public string mACField { get; set; }
    }
}
