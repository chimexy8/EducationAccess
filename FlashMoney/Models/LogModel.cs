using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccess.Models
{
    public class LogModel
    {
        public string time_spent { get; set; }
        public string attempts { get; set; }
        public string authentication { get; set; }
        public string errors { get; set; }
        public string success { get; set; }
        public string mobile { get; set; }
        //public Array input { get; set; }
        public string channel { get; set; }
        //public string history { get; set; }
    }
}
