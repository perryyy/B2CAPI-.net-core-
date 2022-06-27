using B2CAPI.Models;
using B2CAPI.Models.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2CAPI.service;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using B2CAPI.Models.DTOS;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly UploadImg _uploadImg;
        private readonly EditImg _editImg;
        private readonly Switch _Switch;
        public ProductController(B2C_0322Context Perry, UploadImg uploadImg,EditImg editImg, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _uploadImg = uploadImg;
            _editImg = editImg;
            _Switch = Switch;
        }
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ProductImgDTO> Get([FromQuery] string category, string name, string price)
        {

            var result =_Switch.PrintProductImgDTO();
            if (!string.IsNullOrWhiteSpace(price))
            {
                if (price == "High")
                {
                    result = result.OrderByDescending(a => a.price - a.sale);
                }
                else
                {
                    result = result.OrderBy(a => a.price - a.sale);
                }

            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                result = result.Where(a => a.category.Contains(category));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {

                result = result.Where(a => a.name.Contains(name));
            }
            return result;
        }
        [HttpGet("getRandomProduct")]
        [AllowAnonymous]
        public IEnumerable<ProductImgDTO> getRandomProduct()
        {
            var result = _Switch.PrintProductImgDTO();
            Random random = new Random();
            int seed = random.Next();
            result = result.OrderBy(s => (~(s.PID & seed)) & (s.PID | seed)).Take(3); // ^ seed);
            return result;
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public Prod_d_DTO Get(int id)
        {
            var productTmp = _B2C_0322Context.Products.Where(x => x.PID == id).FirstOrDefault();
            var imgTmp = _B2C_0322Context.Imgs.Where(x => x.PID == id).FirstOrDefault();
            var result = _Switch.PrintProductDetails(id, productTmp.PID);
            return result;
        }
        // PUT api/<TestController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files, [FromForm] string jdata)
        {
            Product data = JsonSerializer.Deserialize<Product>(jdata);
            var isOk = _B2C_0322Context.Products.Where(x => x.name == data.name).Count();
            if (isOk == 1)
            {
                var nowData = _B2C_0322Context.Products.Where(x => x.name == data.name).FirstOrDefault();
                nowData.qty +=  1;
                _B2C_0322Context.SaveChanges();
                var result = new Result { Msg = "商品已存在，數量+1", Status = "success" };
                return Ok(result);
            }
            else
            {
                data.btime = DateTime.Now;
                data.buser = 123;
                await _B2C_0322Context.Products.AddAsync(data);
                
                await _B2C_0322Context.SaveChangesAsync();
                int nowPID = data.PID;
                await _uploadImg.upload(files, nowPID);

                var result = new Result { Msg = "成功啦", Status = "success" };
                return Ok(result);
            }
        }
        //[HttpPut("{id}")]
        //public IActionResult Put([FromBody] Product product)
        //{
        //    if (product == null)
        //    {
        //        throw new ArgumentNullException("product");
        //    }
        //    var data = _B2C_0322Context.Products.Select(x => new Product { PID = x.PID }).Where(y => y.PID == product.PID).FirstOrDefault();
        //    if (data == null)
        //    {
        //        var result = new Result { Msg = "失敗啦", Status = "fail" };
        //        return NotFound(result);
        //    }
        //    else
        //    {
        //        _B2C_0322Context.Products.Remove(data);
        //        //member.Auth = "user";
        //        product.ctime = DateTime.Now;
        //        product.cuser = 123;
        //        _B2C_0322Context.Products.Add(product);
        //        _B2C_0322Context.SaveChanges();
        //        var result = new Result { Msg = "成功啦", Status = "success" };
        //        return Ok(result);
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] List<IFormFile> files, [FromForm] string jdata)
        {
            Product data = JsonSerializer.Deserialize<Product>(jdata);
            var nowData = _B2C_0322Context.Products.Where(x => x.PID == data.PID).SingleOrDefault();
            if (data != null)
            {
                int nowPID = data.PID;
                await _editImg.editImg(files, nowPID);
                _B2C_0322Context.Products.Remove(nowData);
                data.ctime = DateTime.Now;
                data.cuser = 123;
                await _B2C_0322Context.Products.AddAsync(data);
                await _B2C_0322Context.SaveChangesAsync();
            }
            var res = _B2C_0322Context.Products.Where(x => x.PID == data.PID).FirstOrDefault();
            return Ok(new { Msg = "修改成功啦", Status = "success", Data = res });
        }
        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = (from a in _B2C_0322Context.Products
                          where a.PID == id
                          select a).SingleOrDefault();
            if (delete != null)
            {
                _B2C_0322Context.Products.Remove(delete);
                _B2C_0322Context.SaveChanges();
                var data = _B2C_0322Context.Products;
                return Ok(data);
            }
            else
            {
                var result = new Result { Msg = "失敗啦", Status = "fail" };
                return NotFound(result);
            }
        }
     }
}
