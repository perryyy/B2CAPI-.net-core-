using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    // OrderDetails Product Img
    public class ODPIDTO
    {
        public int ODID { get; set; }
        public int OID { get; set; }
        public int PID { get; set; }
        public string Prod_name { get; set; }
        public int Prod_price { get; set; }
        public int Prod_qty { get; set; }
        public int Prod_sale { get; set; }
        public int? Prod_DID { get; set; }

        public byte[] img { get; set; }
    }
}
