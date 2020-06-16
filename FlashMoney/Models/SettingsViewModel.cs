using FlashMoney.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class SettingsViewModel
    {
        public ProfileViewModel Profile { get; set; }
        public PasswordChangeViewModel PasswordChange { get; set; }
        public TwoFADTO TwoFADTO { get; set; }
    }
}
