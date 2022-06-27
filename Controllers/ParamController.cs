using B2CAPI.Models;
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
    public class ParamController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly Switch _Switch;
        public ParamController(B2C_0322Context Perry, Switch Switch)
        {
            _B2C_0322Context = Perry;
            _Switch = Switch;
        }
        // GET: api/<ParamController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Param> Get([FromQuery] string category)
        {
            var result = _Switch.PrintParam().OrderBy(x => x.category);
            if (!string.IsNullOrWhiteSpace(category))
            {
                result = result.Where(x => x.category == category && x.status=="Y").OrderBy(x=>x.priority);
            }
            return result;
        }

        // GET api/<ParamController>/5
        [HttpGet("getCategory")]
        [AllowAnonymous]
        public IActionResult getCategory()
        {
            var result = _B2C_0322Context.Params.GroupBy(x =>new { x.category})
                        .Select(x => new
                        {
                            Category = x.Key.category
                        }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public Param Get(int id)
        {
            var result = _B2C_0322Context.Params.Where(x => x.PID == id).FirstOrDefault();
            return result;
        }

        // POST api/<ParamController>
        [HttpPost]
        public IActionResult Post([FromBody] Param data)
        {
            var isOk = _B2C_0322Context.Params.Where(x => x.name == data.name).Count();
            if (isOk == 0)
            {
                int nowPriority = _B2C_0322Context.Params.Where(x => x.category == data.category).Max(x => x.priority)+1;
                data.Btime = DateTime.Now;
                data.Buser = 123;
                data.priority = nowPriority;
                _B2C_0322Context.Params.Add(data);
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintParam();
                return Ok(new { Msg = "成功啦", Status = "success", Data = result });
            }
            else
            {
                var result = _Switch.PrintParam();
                return Ok(new { Msg = "失敗啦", Status = "fail", Data = result });
            }
        }

        // PUT api/<ParamController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Param param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("Params");
            }
            var data = _B2C_0322Context.Params.Select(x => new Param { PID = x.PID }).Where(y => y.PID == param.PID).FirstOrDefault();
            if (data == null)
            {
                return Ok(new { Msg = "失敗啦", Status = "fail"});
            }
            else
            {
                _B2C_0322Context.Params.Remove(data);
                param.Ctime = DateTime.Now;
                param.Cuser = 123;
                _B2C_0322Context.Params.Add(param);
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintParam().OrderBy(x => x.category);
                return Ok(new { Msg = "成功啦", Status = "success", Data = result });
            }
        }

        // DELETE api/<ParamController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = (from a in _B2C_0322Context.Params
                          where a.PID == id
                          select a).SingleOrDefault();
            if (delete != null)
            {
                _B2C_0322Context.Params.Remove(delete);
                _B2C_0322Context.SaveChanges();
                var result = _Switch.PrintParam().OrderBy(x => x.category);
                return Ok(new { Msg = "成功啦", Status = "success", Data = result });
            }
            else
            {
                var result = _Switch.PrintParam().OrderBy(x => x.category);
                return Ok(new { Msg = "失敗啦", Status = "fail", Data = result });
            }
        }
    }
}
