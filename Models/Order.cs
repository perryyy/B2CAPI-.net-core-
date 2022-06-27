using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Order
    {
        public int OID { get; set; }
        public int MID { get; set; }
        public int total { get; set; }
        public int discount { get; set; }
        public int freight { get; set; }
        public int additional_fee { get; set; }
        public DateTime time { get; set; }
        public DateTime order_date { get; set; }
        public string order_remark { get; set; }
        public string order_status { get; set; }
        public string customer_name { get; set; }
        public string customer_phone { get; set; }
        public string deliver_name { get; set; }
        public string deliver_methods { get; set; }
        public string deliver_phone { get; set; }
        public string deliver_address { get; set; }
        public string deliver_status { get; set; }
        public string pay_method { get; set; }
        public string pay_status { get; set; }
        public int buser { get; set; }
        public DateTime btime { get; set; }
        public int? cuser { get; set; }
        public DateTime? ctime { get; set; }
    }
}
