using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAccessApi.Models
{
    public class ConfirmationAwaiting
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Time { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
    }
}
