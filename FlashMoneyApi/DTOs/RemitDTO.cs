using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class RemitDTO
    {
        public string BeneficiaryPhone { get; set; }
        public string Amount { get; set; }
        public string Narration { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
    }
}
