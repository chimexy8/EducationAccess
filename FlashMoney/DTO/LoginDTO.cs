using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.DTO
{
    public class LoginDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }

        public string Phone { get; set; }
        public string Token { get; set; }
        public string Pin { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
        public string session { get; set; }


    }
}
