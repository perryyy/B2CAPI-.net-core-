using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class OrderDTO
    {
        public ICollection<Order> order { get; set; }
        public IEnumerable<OrderDetail> orderdetails { get; set; }
    }
}
