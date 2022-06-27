using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class OrderDetail
    {
        public int ODID { get; set; }
        public int OID { get; set; }
        public int PID { get; set; }
        public string Prod_name { get; set; }
        public int Prod_price { get; set; }
        public int Prod_qty { get; set; }
        public int Prod_sale { get; set; }
        public int? Prod_DID { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int buser { get; set; }
        public int? cuser { get; set; }
    }
}
