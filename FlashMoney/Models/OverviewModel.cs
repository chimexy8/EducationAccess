using FlashMoney.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class OverviewModel
    {
        public string Balance { get; set; }
        public int TransactionCount { get; set; }
        public int FundWalletCount { get; set; }
        public string LastFunded { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public List<ActivityDTO> Activities { get; set; }


        public TransactionModel TransactionModel { get; set; }


        public ProfileViewModel Profile { get; set; }
        public PasswordChangeViewModel PasswordChange { get; set; }
        public TwoFADTO TwoFADTO { get; set; }

        public MultipleTransModel MultipleTransModel { get; set; }

        [BindProperty]
        public List<MultipleTransModel> Recipients { get; set; }
    }
}
