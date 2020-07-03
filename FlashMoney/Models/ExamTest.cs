using FlashMoney.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class ExamTest
    {
        public int Id { get; set; }
        public int ExamTypeId { get; set; }
        public List<Subject> Subjects{ get; set; }
        public ExamType ExamType { get; set; }
        public decimal Score { get; set; }
        public int UserId { get; set; }
        public DateTime ExamDate { get; set; }
        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }

    }
}
