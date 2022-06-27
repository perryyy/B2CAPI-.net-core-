using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class Img
    {
        public int IID { get; set; }
        public int PID { get; set; }
        public string Img_name { get; set; }
        public string Img_path { get; set; }
        public string status { get; set; }
        public int Buser { get; set; }
        public int? Cuser { get; set; }
        public DateTime Btime { get; set; }
        public DateTime? Ctime { get; set; }
        public string spare { get; set; }
    }
}
