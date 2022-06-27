using B2CAPI.Models;
using B2CAPI.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTLoginController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly JWT _JWT;
        public JWTLoginController(B2C_0322Context Perry, JWT jwt)
        {
            _B2C_0322Context = Perry;
            _JWT = jwt;
        }

        // POST api/<JWTLoginController>
        [HttpPost]
        public string Post([FromBody] Member value)
        {
            if (value != null)
            {
                var tk = _JWT.MakeJWT(value);
                return tk;

            }
            else {
                return "失敗";
            }
        }
    }
}
