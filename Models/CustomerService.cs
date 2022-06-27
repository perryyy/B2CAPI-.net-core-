using System;
using System.Collections.Generic;

#nullable disable

namespace B2CAPI.Models
{
    public partial class CustomerService
    {
        public int Csid { get; set; }
        public int Mid { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime? Ctime { get; set; }
        public DateTime Btime { get; set; }
        public int Buser { get; set; }
        public int? Cuser { get; set; }
    }
}
