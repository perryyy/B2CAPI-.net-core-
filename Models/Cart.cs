using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Cart
    {
        public int CID { get; set; }
        public int PID { get; set; }
        public int MID { get; set; }
        public int qty { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int? cuser { get; set; }
        public int buser { get; set; }
    }
}
