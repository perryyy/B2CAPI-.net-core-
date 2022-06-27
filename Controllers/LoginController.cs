using B2CAPI.Models;
using B2CAPI.Models.DTOS;
using B2CAPI.Models.Return;
using B2CAPI.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly AddPassword _addPassword;
        private readonly JWT _JWT;
        public LoginController(B2C_0322Context Perry, AddPassword addPassword, JWT jwt)
        {
            _B2C_0322Context = Perry;
            _addPassword = addPassword;
            _JWT = jwt;
        }
        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] Member data)
        {
            data.password = _addPassword.HashPassword(data.password);
            var isOk = _B2C_0322Context.Members.Where(x => x.username == data.username && x.password == data.password).FirstOrDefault();

            if (isOk != null && isOk.status=="Y")
            {
                var result = _B2C_0322Context.Members.Select(x => new memberDTO
                {
                    Mid = x.MID,
                    Username = x.username,
                    Sex = x.sex,
                    Phone = x.phone,
                    Email = x.email,
                    Birth = x.birth,
                    auth = x.auth,
                    token = _JWT.MakeJWT(data)
                }).Where(x => x.Mid == isOk.MID).FirstOrDefault();
                if (DateTime.Now < isOk.startdate || DateTime.Now > isOk.enddate)
                {
                    isOk.status = "N";
                    _B2C_0322Context.SaveChanges();
                }
                return Ok(new { Msg = "登入成功啦", Status = "success", Data = result });
            }
            else
            {
                return Ok(new { Msg = "登入失敗啦", Status = "fail"});
            }
        }
    }
}
