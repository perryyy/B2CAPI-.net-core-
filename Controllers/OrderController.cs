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
    public class OrderController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public OrderController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            var result = _Switch.PrintOrder();
            return result;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IEnumerable<Order> Get(int id)
        {
            var result = _Switch.PrintOrder().Where(z=>z.MID==id);
            return result;
        }
        [HttpGet("getMemberTotal/{id}")]
        public int getMemberTotal(int id)
        {
            var result = _Switch.PrintOrder().Where(z => z.MID == id && z.order_status == "Y");
            var total = 0;
            foreach (var item in result)
            {
                total += item.total;
            }
            return total;
        }
        [HttpGet("getSingle/{id}")]
        public IEnumerable<Order> GetSingle(int id)
        {
            var result = _B2C_0322Context.Orders.Select(x => new Order
            {
                OID = x.OID,
                total = x.total,
                MID = x.MID,
                discount=x.discount,
                freight=x.freight,
                additional_fee=x.additional_fee,
                order_date = x.order_date,
                order_status = x.order_status,
                order_remark=x.order_remark,
                customer_name=x.customer_name,
                customer_phone=x.customer_phone,
                deliver_name=x.deliver_name,
                deliver_address=x.deliver_address,
                deliver_methods=x.deliver_methods,
                deliver_phone=x.deliver_phone,
                deliver_status=x.deliver_status,
                pay_method=x.pay_method,
                pay_status=x.pay_status
            }).Where(z => z.OID == id);
            return result;
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO data)
        {
            int NowMID = 0;
            var result = new Order();
            //寫入order
            if (data.order != null) {
                foreach (var i in data.order)
                {
                    NowMID = i.MID;
                    result.btime = DateTime.Now;
                    result.buser = 123;
                    result.MID = i.MID;
                    result.total = i.total;
                    result.discount = i.discount;
                    result.additional_fee = i.additional_fee;
                    result.time = DateTime.Now;
                    result.order_date = DateTime.Now;
                    result.order_remark = i.order_remark;
                    result.order_status = "N";
                    result.customer_name = i.customer_name;
                    result.customer_phone = i.customer_phone;
                    result.deliver_name = i.deliver_name;
                    result.deliver_methods = i.deliver_methods;
                    result.deliver_address = i.deliver_address;
                    result.deliver_phone = i.deliver_phone;
                    result.deliver_status = "N";
                    result.pay_method = i.pay_method;
                    result.pay_status = "N";
                    _B2C_0322Context.Orders.Add(result);

                    
                }
                _B2C_0322Context.SaveChanges();
            }
            //寫入orderDetails
            if (data.orderdetails != null) {
                var oid = _B2C_0322Context.Orders.Select(x => x.OID).Max();
                var newOID = result.OID;
                foreach (var i in data.orderdetails)
                {
                    var odResult = new OrderDetail();
                    odResult.PID = i.PID;
                    odResult.Prod_name = i.Prod_name;
                    odResult.Prod_price = i.Prod_price;
                    odResult.Prod_qty = i.Prod_qty;
                    odResult.Prod_sale = i.Prod_sale;
                    odResult.Prod_DID = i.Prod_DID;
                    odResult.OID = newOID;
                    odResult.buser = 123;
                    odResult.btime = DateTime.Now;
                    _B2C_0322Context.OrderDetails.Add(odResult);
                    var nowProduct = _B2C_0322Context.Products.Where(x => x.PID == i.PID).FirstOrDefault();
                    nowProduct.qty = nowProduct.qty - i.Prod_qty;
                }
            }
            //刪除購物車內容
            var delete = (from a in _B2C_0322Context.Carts
                          where a.MID == NowMID
                          select a).ToList();
            _B2C_0322Context.Carts.RemoveRange(delete);
            _B2C_0322Context.SaveChanges();
            //回傳目前購物車
            var returnData = _Switch.PrintCart(NowMID);
            return Ok(new { Msg = "新增成功啦", Status = "success",data= returnData});
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
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
            //product qty 新增
            foreach (var item in deleteOrderDetails)
            {
                var nowProduct = _B2C_0322Context.Products.Where(x => x.PID == item.PID).FirstOrDefault();
                nowProduct.qty = nowProduct.qty + item.Prod_qty;
            }
            _B2C_0322Context.SaveChanges();
        }
    }
}
