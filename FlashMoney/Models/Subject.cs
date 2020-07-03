using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExamTestId { get; set; }
        public ExamTest ExamTest { get; set; }
        public List<Question> Questions { get; set; }
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
        public int CategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }
        public int ExamScholarshipId { get; set; }
        public ExamScholarship ExamScholarship { get; set; }

    }
}
