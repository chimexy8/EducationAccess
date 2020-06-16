using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class SettingsDTO
    {
        public ProfileDTO Profile { get; set; }
        public PasswordChangeDTO PasswordChange { get; set; }
    }
}
