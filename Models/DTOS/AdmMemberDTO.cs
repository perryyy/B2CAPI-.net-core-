using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class AdmMemberDTO
    {
        public int MID { get; set; }
        public string username { get; set; }
        public string sex { get; set; }
        public DateTime? birth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string auth { get; set; }

        //可直接傳是或否給前端
        //private string _status { get; set; }
        //public string status
        //{
        //    get
        //    {
        //        if (_status == "Y")
        //        {
        //            return "是";
        //        }
        //        else
        //        {
        //            return "否";
        //        }
        //    }
        //    set { _status = value; }
        //}
        public string status { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}
