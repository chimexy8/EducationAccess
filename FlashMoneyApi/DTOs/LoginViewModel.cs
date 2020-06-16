﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class LoginViewModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
