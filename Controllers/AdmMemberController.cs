using B2CAPI.Models;
using B2CAPI.Models.DTOS;
using B2CAPI.service;
using Microsoft.AspNetCore.Authorization;
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
    public class AdmMemberController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly AddPassword _addPassword;
        private readonly Switch _Switch;
        public AdmMemberController(B2C_0322Context Perry, AddPassword addPassword, Switch Switch)
        {
            _addPassword = addPassword;
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<AdmMemberController>
        [HttpGet]
        public IEnumerable<AdmMemberDTO> Get([FromQuery] string name)
        {
            var result =_Switch.PrintAdmMember();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(a => a.username.Contains(name));
            }
            return result;
        }

        // GET api/<AdmMemberController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _Switch.PrintAdmMember().Where(a=>a.MID==id).FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        // POST api/<AdmMemberController>
        [HttpPost]
        public IActionResult Post([FromBody] Member data)
        {

            var isOk = _B2C_0322Context.Members.Where(x => x.username == data.username).Count();
            if (isOk == 0)
            {
                data.btime = DateTime.Now;
                data.password = _addPassword.HashPassword(data.password);
                data.buser = 123;
                _B2C_0322Context.Members.Add(data);
                _B2C_0322Context.SaveChanges();
                return Ok(new { Msg = "新增成功啦", Status = "success"});
            }
            else
            {
                return Ok(new { Msg = "修改成功啦", Status = "success"});
            }
        }

        // PUT api/<AdmMemberController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member");
            }
            var data = (from a in _B2C_0322Context.Members
                        where a.MID == member.MID
                        select a).FirstOrDefault();
            if (data == null)
            {
                return Ok(new { Msg = "找不到資料", Status = "Fail"});
            }
            else
            {
                data.username = member.username;
                data.MID = member.MID;
                data.email = member.email;
                data.phone = member.phone;
                data.sex = member.sex;
                data.birth = member.birth;
                data.auth = member.auth;
                data.status = member.status;
                data.startdate = member.startdate;
                data.enddate = member.enddate;
                data.ctime = DateTime.Now;
                data.cuser = 456;

                _B2C_0322Context.SaveChanges();
                var content = _Switch.PrintAdmMember().Where(a => a.MID == data.MID).FirstOrDefault();
                return Ok(new { Msg = "修改成功啦", Status = "success", Data = content });
            }

        }

        // DELETE api/<AdmMemberController>/5
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
                return Ok(new { Msg = "刪除失敗啦", Status = "Fail" });
            }
        }
    }
}
