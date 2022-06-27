using B2CAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2CAPI.service;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly AddPassword _addPassword;
        public PasswordController(B2C_0322Context Perry , AddPassword addPassword)
        {
            _addPassword = addPassword;
            _B2C_0322Context = Perry;
        }
        // GET: api/<PasswordController>
        [HttpGet]
        public IActionResult Get([FromBody] Member data)
        {
            if (data != null) {
                var  nowpsw = _addPassword.HashPassword(data.password);
                var result = _B2C_0322Context.Members.Where(x => x.MID == data.MID && x.password == nowpsw).FirstOrDefault();
                if (result != null) {
                    return Ok(new { Msg = "你可以修改啦", Status = "success"});
                }
            }
            return Ok(new { Msg = "你不能修改啦", Status = "Fail" });
        }

        // POST api/<PasswordController>
        [HttpPost]
        public IActionResult Post([FromBody] Member data)
        {
            if (data != null)
            {
                var nowpsw = _addPassword.HashPassword(data.password);
                var result = _B2C_0322Context.Members.Where(x => x.MID == data.MID && x.password == nowpsw).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new { Msg = "你可以修改啦", Status = "success" });
                }
            }
            return Ok(new { Msg = "你不能修改啦", Status = "Fail" });
        }

        // PUT api/<PasswordController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("product");
            }
            var data = _B2C_0322Context.Members.Where(y => y.MID == member.MID).FirstOrDefault();
            if (data == null)
            {
                return Ok(new { Msg = "修改失敗啦", Status = "success" });
            }
            else
            {
                var newpasswrd = _addPassword.HashPassword(member.password);
                member.password = newpasswrd;
                member.ctime = DateTime.Now;
                member.cuser = 123;
                member.changepswdate = DateTime.Now;
                member.sex = data.sex;
                member.username = data.username;
                member.birth = data.birth;
                member.phone = data.phone;
                member.email = data.email;
                member.auth = data.auth;
                member.btime = data.btime;
                member.buser = data.buser;
                member.status = data.status;
                member.startdate = data.startdate;
                member.enddate = data.enddate;
                _B2C_0322Context.Members.Remove(data);
                _B2C_0322Context.Members.Add(member);
                _B2C_0322Context.SaveChanges();
                return Ok(new { Msg = "修改成功啦", Status = "success" });
            }
        }
    }
}
