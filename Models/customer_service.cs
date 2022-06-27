using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class customer_service
    {
        public int CSID { get; set; }
        public int MID { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int buser { get; set; }
        public int? cuser { get; set; }
    }
}
