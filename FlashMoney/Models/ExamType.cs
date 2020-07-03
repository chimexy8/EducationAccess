using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class ExamType
    {
        public int Id { get; set; }
        public string ExamTypeName { get; set; }
        public List<ExamTest> ExamTests { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
