using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Member
    {
        public int MID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string sex { get; set; }
        public DateTime? birth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string auth { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int buser { get; set; }
        public int? cuser { get; set; }
        public DateTime? changepswdate { get; set; }
        public string status { get; set; }

        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}
