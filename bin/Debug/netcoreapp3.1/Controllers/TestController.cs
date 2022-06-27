using B2CAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        public TestController(B2C_0322Context Perry)
        {
            _B2C_0322Context = Perry;
        }
        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<Product> Get([FromQuery] string category, string name)
        {
            var result = _B2C_0322Context.Products.Select(x => new Product
            {
                PID = x.PID,
                name = x.name,
                price = x.price,
                sale = x.sale,
                category = x.category,
                qty = x.qty,
                description = x.description
            });
            if (!string.IsNullOrWhiteSpace(category) )
            {
               result = result.Where(a => a.category.Contains(category));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {

               result = result.Where(a => a.name.Contains(name));
            }
            return result;
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var result = _B2C_0322Context.Products.Where(x => x.PID == id).FirstOrDefault();
            return result;
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
