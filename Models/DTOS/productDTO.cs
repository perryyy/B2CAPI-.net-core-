using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class productDTO
    {
        public int Pid { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Sale { get; set; }
        public string Category { get; set; }
        public string orderby { get; set; }

    }
}
