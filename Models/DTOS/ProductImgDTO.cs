using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class ProductImgDTO
    {
        public int PID { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int sale { get; set; }
        public string category { get; set; }
        public int qty { get; set; }
        public string description { get; set; }
        public DateTime? ctime { get; set; }
        public DateTime btime { get; set; }
        public int buser { get; set; }
        public int? cuser { get; set; }
        public int DID { get; set; }
        public string spare { get; set; }
        public ICollection<Img> file_path { get; set; }
        public ICollection<Img> file_name { get; set; }
        public byte[] img { get; set; }
    }
}
