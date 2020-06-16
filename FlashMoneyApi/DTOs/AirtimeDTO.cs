using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.DTOs
{
    public class AirtimeDTO
    {
        public string SourcePhone { get; set; }
        public string DestinationPhone { get; set; }
        public decimal Amount { get; set; }
        public NetWork NetWork { get; set; }
        public int Pin { get; set; }
    }

    public enum NetWork
    {
        MTN,Glo,Etisalat,Airtel
    }
}
