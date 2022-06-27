using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Discount
    {
        public int DID { get; set; }
        public string name { get; set; }
        public decimal percentage { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Buser { get; set; }
        public DateTime Btime { get; set; }
        public int? Cuser { get; set; }
        public DateTime? Ctime { get; set; }
    }
}
