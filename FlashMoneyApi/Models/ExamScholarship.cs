using FlashMoneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccessApi.Models
{
    public class ExamScholarship
    {
        public int Id { get; set; }
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }
        public int UserId { get; set; }
        
        public decimal Score { get; set; }
        public int SponsorId { get; set; }
        public User User { get; set; }
        public int ScholarshipApplicationId { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}
