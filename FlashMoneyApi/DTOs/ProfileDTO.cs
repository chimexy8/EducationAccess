using FlashMoneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class ProfileDTO
    {

        public string FirstName { get; set; }
        
        public string MothersMedianName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string DOB { get; set; }
        
        public string Gender { get; set; }
        
        public string Phone { get; set; }

        public string Passport { get; set; }
        public string BVN { get; set; }

        public string NextofKinFirstname { get; set; }

        public string NextofKinLastName { get; set; }

        public string NextofKinPhone { get; set; }

        public string NextofKinEmail { get; set; }

        public string NextofKinAddress { get; set; }

        public bool HasTransactionPin { get; set; }
        public bool HasAuthPin { get; set; }

        public bool AllowTransactionNotif { get; set; }
        public bool AllowActivityNotif { get; set; }

        public DateTime LastWalletFundedDate { get; set; }
    }
}
