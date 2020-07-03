using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class ScholarshipApplication
    {
        public int Id { get; set; }
        public Enum.Scholarshiprecipient ScholarshipType { get; set; }
        public string Name { get; set; }
        public int ScholarshipCategoryId { get; set; }
        public ScholarshipCategory ScholarshipCategory { get; set; }
        public DateTime Date_Time { get; set; }
        public decimal AmountForEach { get; set; }
        public string AboutScholarship { get; set; }
        public string NumberOfPeople { get; set; }
        public bool SponsorProvideExam { get; set; }
        public Enum.ApplicationStatus ApplicationStatus { get; set; }

        public bool Active { get; set; }
        public bool MadePayment { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
