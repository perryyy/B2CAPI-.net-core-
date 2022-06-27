using B2CAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2CAPI.Models.DTOS;
using System.Net.Mail;
using B2CAPI.service;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly SentMail _sentmail;
        public ExceptionController(B2C_0322Context Perry, SentMail sentmail)
        {
            _B2C_0322Context = Perry;
            _sentmail = sentmail;
        }
        // GET: api/<ExceptionController>
        [HttpGet]
        public IEnumerable<ProductImgDTO> Get([FromQuery] string category, [FromQuery] string orderby)
        {
            var result = (from p in _B2C_0322Context.Products
                          let i = _B2C_0322Context.Imgs.Where(x => x.PID == p.PID).OrderBy(x => x.PID).FirstOrDefault()
                          where i.Img_name != null
                          select new ProductImgDTO
                          {
                              PID = p.PID,
                              price = p.price,
                              sale = p.sale,
                              name = p.name,
                              category = p.category,
                              qty = p.qty,
                              img = System.IO.File.ReadAllBytes(i.Img_path + i.Img_name)
                          });
            if (!string.IsNullOrWhiteSpace(category))
            {
                if (orderby== "High")
                {
                    result = result.Where(x => x.category.Contains(category)).OrderByDescending(a => a.price - a.sale);
                }
                else
                {
                    result = result.Where(x => x.category.Contains(category)).OrderBy(a => a.price - a.sale);
                }

            }
            return result;
        }

        // POST api/<ExceptionController>
        //忘記密碼
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Member data)
        {
            if (data != null) {
                var result = _B2C_0322Context.Members.Where(x => x.username == data.username && x.email == x.email).FirstOrDefault();
                if (result != null)
                {
                    _sentmail.sentmail(result);
                    return Ok(new { Msg = "傳送成功啦", Status = "success" });
                }
                else
                {
                    return Ok(new { Msg = "傳送失敗啦1", Status = "fail" });

                }
            }
            return Ok(new { Msg = "傳送失敗啦", Status = "fail" });
        }

    }
}
