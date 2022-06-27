using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Param
    {
        public int PID { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int Buser { get; set; }
        public DateTime Btime { get; set; }
        public int? Cuser { get; set; }
        public DateTime? Ctime { get; set; }
        public string spare { get; set; }
        public int priority { get; set; }
    }
}
