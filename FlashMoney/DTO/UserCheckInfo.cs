using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class UserCheckInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public bool HasSetTap { get; set; }
        public string Status { get; set; }
        public string ReasonPhrase { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

    }
}
