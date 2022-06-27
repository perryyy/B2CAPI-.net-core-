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
    public class CartController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public CartController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<CartDTO> Get()
        {
            var result = _Switch.PrintCartNoParam();
            return result;


        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public IEnumerable<CartDTO> Get(int id)
        {
            var result = _Switch.PrintCart(id);
            return result;
        }

        // POST api/<CartController>
        [HttpPost]
        public IActionResult Post([FromBody] Cart data)
        {

            var isOk = _B2C_0322Context.Carts.Where(x => x.PID == data.PID && x.MID == data.MID).FirstOrDefault();
            if (isOk == null)
            {
                data.btime = DateTime.Now;
                data.buser = 123;
                data.qty = 1;
                _B2C_0322Context.Carts.Add(data);
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintCart(data.MID);
                return Ok(new { Msg = "新增成功啦", Status = "success", data = result });
            }
            else
            {
                isOk.ctime = DateTime.Now;
                isOk.cuser = 123;
                isOk.qty = isOk.qty + 1;
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintCart(isOk.MID);
                return Ok(new { Msg = "新增成功啦,多了一筆", Status = "success", data = result });
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = (from a in _B2C_0322Context.Carts
                          where a.CID == id
                          select a).SingleOrDefault();
            if (delete != null)
            {
                _B2C_0322Context.Carts.Remove(delete);
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintCart(delete.MID);
                return Ok(new { Msg = "刪除成功啦", Status = "success", data = result });
            }
            else
            {
                return Ok(new { Msg = "刪除失敗啦", Status = "fail" });
            }
        }
        [HttpPost("AddCarts")]
        public IActionResult AddCarts([FromBody] IEnumerable<Cart> data, [FromQuery] int MID)
        {
            foreach (var item in data)
            {
                var isOk = _B2C_0322Context.Carts.Where(x => x.PID == item.PID && x.MID == item.MID).FirstOrDefault();
                if (isOk == null)
                {
                    item.btime = DateTime.Now;
                    item.buser = 123;
                    item.qty = 1;
                    _B2C_0322Context.Carts.Add(item);
                }
                else
                {
                    isOk.ctime = DateTime.Now;
                    isOk.cuser = 123;
                    isOk.qty = isOk.qty + 1;
                }
            }
            _B2C_0322Context.SaveChanges();
            var result = _Switch.PrintCart(MID);
            return Ok(new { Msg = "新增成功啦", Status = "success", data = result });
        }

    }
}
