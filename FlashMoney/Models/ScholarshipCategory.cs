using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class ScholarshipCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ScholarshipApplication> ScholarshipApplications { get; set; }
    }
}
