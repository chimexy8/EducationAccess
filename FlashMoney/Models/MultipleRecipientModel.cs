using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoney.Models
{
    public class MultipleRecipientModel
    {
        public List<MultipleTransModel> Recipients { get; set; } = new List<MultipleTransModel>();

        public string SourcePhone { get; set; }

        public string AuthType { get; set; }

        public string Pin { get; set; }


        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }

        public string PinOne { get; set; }
        public string PinTwo { get; set; }
        public string PinThree { get; set; }
        public string PinFour { get; set; }
    }
}
