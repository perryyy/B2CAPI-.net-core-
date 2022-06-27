using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class CartDTO
    {
        public int CID { get; set; }
        public ICollection PID { get; set; }
        public ICollection MID { get; set; }
        public int qty { get; set; }
    }
}
