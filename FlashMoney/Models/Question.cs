using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string ExamQuestion { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
    }
}
