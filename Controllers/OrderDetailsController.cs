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
    public class OrderDetailsController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public OrderDetailsController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET api/<OrderDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<ODPIDTO> Get(int id)
        {
            var result = _Switch.PrintOrderDetails(id);
            return result;
        }
    }
}
