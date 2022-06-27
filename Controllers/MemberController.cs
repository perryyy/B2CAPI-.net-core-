using B2CAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using B2CAPI.Models.DTOS;
using B2CAPI.Models.Return;
using B2CAPI.service;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly AddPassword _addPassword;
        private readonly Switch _Switch;
        public MemberController(B2C_0322Context Perry, AddPassword addPassword, Switch Switch)
        {
            _addPassword = addPassword;
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<Member1Controller>
        [HttpGet]
        public IEnumerable<memberDTO> Get([FromQuery] string name)
        {
            var result = _Switch.PrintMember();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(a => a.Username.Contains(name));
            }
            return result;
        }

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _Switch.PrintMember().Where(a=>a.Mid==id).FirstOrDefault();
            if (result != null)
            {
                //var result = new Result { Msg = "成功啦", Status = "success" };
                return Ok(result);
            }
            else
            {
                //var result = new Result { Msg = "失敗啦", Status = "fail" };
                return NotFound(result);
            }
        }

        // POST api/<MemberController>
        //普通會員註冊
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Member data)
        {

            var isOk = _B2C_0322Context.Members.Where(x => x.username == data.username).Count();
            if (isOk == 0)
            {
                data.startdate = DateTime.Now;
                data.enddate = DateTime.Now.AddYears(100);
                data.status = "Y";
                data.btime = DateTime.Now;
                data.password = _addPassword.HashPassword(data.password);
                data.buser = 123;
                _B2C_0322Context.Members.Add(data);
                _B2C_0322Context.SaveChanges();
                var result = new Result { Msg = "成功啦", Status = "success" };
                return Ok(result);
            }
            else
            {
                var result = new Result { Msg = "失敗啦", Status = "fail" };
                return Ok(result);
            }
        }
        [HttpPost("admAdd")]
        public IActionResult admAdd([FromBody] Member data) {
            var isOk = _B2C_0322Context.Members.Where(x => x.username == data.username).Count();
            if (isOk == 0)
            {
                data.btime = DateTime.Now;
                data.password = _addPassword.HashPassword(data.password);
                data.buser = 123;
                _B2C_0322Context.Members.Add(data);
                _B2C_0322Context.SaveChanges();
                var result = new Result { Msg = "成功啦", Status = "success" };
                return Ok(result);
            }
            else
            {
                var result = new Result { Msg = "失敗啦", Status = "fail" };
                return Ok(result);
            }
        }

        // PUT api/<MemberController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Member member)
        {
            if (member == null) {
                throw new ArgumentNullException("member");
            }
            var data = (from a in _B2C_0322Context.Members
                        where a.MID == member.MID
                        select a).FirstOrDefault();
            if (data == null)
            {
                var result = new Result { Msg = "失敗啦", Status = "fail" };
                return NotFound(result);
            }
            else {
                data.username = member.username;
                data.MID = member.MID;
                data.email = member.email;
                data.phone = member.phone;
                data.sex = member.sex;
                data.birth = member.birth;
                data.ctime = DateTime.Now;
                data.cuser = 123;
                data.auth = member.auth;
                _B2C_0322Context.SaveChanges();
                var content = _B2C_0322Context.Members.Where(y => y.MID == member.MID).FirstOrDefault();
                return Ok(new { Msg = "修改成功啦", Status = "success", Data = content });
            }

        }

        // DELETE api/<MemberController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = (from a in _B2C_0322Context.Members
                          where a.MID == id
                          select a).SingleOrDefault();
            if (delete != null)
            {
                _B2C_0322Context.Members.Remove(delete);
                _B2C_0322Context.SaveChanges();
                var data = _B2C_0322Context.Members;
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
