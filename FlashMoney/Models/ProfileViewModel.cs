using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlashMoney.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Mother's Median Name")]
        public string MothersMedianName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }
        
        public string Gender { get; set; }
        
        public string Phone { get; set; }

        public string BVN { get; set; }
        public IFormFile PictureFile { get; set; }
        public string Passport { get; set; }

        public string NextofKinFirstname { get; set; }

        public string NextofKinLastName { get; set; }

        public string NextofKinPhone { get; set; }

        public string NextofKinEmail { get; set; }

        public string NextofKinAddress { get; set; }

        public bool HasTransactionPin { get; set; }
        public bool HasAuthPin { get; set; }

        public bool AllowTransactionNotif { get; set; }
        public bool AllowActivityNotif { get; set; }
    }
}