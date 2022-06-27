using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Product
    {
        public int PID { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int sale { get; set; }
        public string category { get; set; }
        public int qty { get; set; }
        public string description { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int buser { get; set; }
        public int? cuser { get; set; }
        public int DID { get; set; }
        public string spare { get; set; }
    }
}
