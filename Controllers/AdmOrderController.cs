using B2CAPI.Models;
using B2CAPI.Models.DTOS;
using B2CAPI.service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmOrderController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public AdmOrderController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<AdmOrderController>
        [HttpGet]
        public IEnumerable<HistoryOrder> Get([FromQuery] string name)
        {
            var result = _Switch.PrintHistoryOrder();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(a => a.buyer.Contains(name));
            }
            return result;
        }

        // DELETE api/<AdmOrderController>/5
        [HttpDelete("{id}")]
        public IEnumerable<HistoryOrder> Delete(int id)
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
            var result = _Switch.PrintHistoryOrder();
            return result;
        }
    }
}
