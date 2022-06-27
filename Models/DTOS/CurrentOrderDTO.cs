using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class CurrentOrderDTO
    {
        public int OID { get; set; }
        public string buyer { get; set; }
        public int total { get; set; }
        public string order_time { get; set; }
    }
}
