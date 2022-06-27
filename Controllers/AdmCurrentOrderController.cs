using B2CAPI.Models;
using B2CAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Switch = B2CAPI.service.Switch;

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmCurrentOrderController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public AdmCurrentOrderController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        [HttpGet]
        public IEnumerable<CurrentOrderDTO> Get([FromQuery] string name)
        {
            //var result = _B2C_0322Context.Orders.Where(x => x.order_status.Contains("N")).ToList();
            var result = _Switch.PrintNowOrder();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(a => a.buyer.Contains(name));
            }
            return result;
        }
        [HttpPut("{id}")]
        public IEnumerable<CurrentOrderDTO> Put(int id)
        {
            var tmp = _B2C_0322Context.Orders.Where(x => x.OID == id).FirstOrDefault();
            tmp.order_status = "Y";
            tmp.ctime = DateTime.Now;
            tmp.cuser = 123;
            _B2C_0322Context.SaveChanges();
            var result = _Switch.PrintNowOrder();
            return result;
        }
        [HttpDelete("{id}")]
        public IEnumerable<CurrentOrderDTO> Delete(int id)
        {
            //刪除order
            var deleteOrder = (from a in _B2C_0322Context.Orders
                               where a.OID == id
                               select a).SingleOrDefault();
            if (deleteOrder != null)
            {
                _B2C_0322Context.Orders.Remove(deleteOrder);
            }
            //刪除orderdetails
            var deleteOrderDetails = (from a in _B2C_0322Context.OrderDetails
                                      where a.OID == deleteOrder.OID
                                      select a).ToList();
            _B2C_0322Context.OrderDetails.RemoveRange(deleteOrderDetails);

            _B2C_0322Context.SaveChanges();
            //回傳當前訂單
            var result = _Switch.PrintNowOrder();
            return result;
        }
    }
}
