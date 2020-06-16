using FlashMoney.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class AuthenticationModel
    {
        public BvnOrPhoneModel BvnOrPhoneModel { get; set; }
        public ResetDTO ResetDTO { get; set; }
        public SignInViewModel SignInViewModel { get; set; }
    }
}
