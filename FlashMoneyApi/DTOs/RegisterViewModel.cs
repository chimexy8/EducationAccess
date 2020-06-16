using FlashMoneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class RegisterViewModel
    {
       
        public string FirstName { get; set; }
        public string MothersMedianName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string date { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Pin { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }

        public DateTime DOB { get; set; }
    }
}
