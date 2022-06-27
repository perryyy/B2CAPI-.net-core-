using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class PerformanceDTO
    {
        //商品名稱
        public string name { get; set; }
        //商品單價
        public int price { get; set; }
        //商品種類
        public string category { get; set; }
        //商品銷售數量
        public int qty { get; set; }
        //商品銷售總額
        public int total { get; set; }
    }
}
