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
    public class PerformanceController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public PerformanceController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;

        }
        // GET: api/<PerformanceController>
        [HttpGet]
        public IActionResult Get([FromQuery] DateTime Btime, DateTime Etime)
        {
            var totalOrderDetails = (from p in _B2C_0322Context.Products
                                     select new PerformanceDTO
                                     {
                                         name = p.name,
                                         price = p.price,
                                         qty = _Switch.PrintSum(Btime, Etime,p.PID),
                                         category = p.category,
                                         total = p.price * _Switch.PrintSum(Btime, Etime, p.PID)
                                     } 
                           ).ToList();
            return Ok(new { data = totalOrderDetails });
        }
    }
}
