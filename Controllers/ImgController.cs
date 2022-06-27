using B2CAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace B2CAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly B2C_0322Context _B2C_0322Context;
        public ImgController(B2C_0322Context Perry)
        {
            _B2C_0322Context = Perry;
        }
        // GET: api/<ImgController>
        [HttpGet]
        public IEnumerable<Img> Get()
        {
            var result = _B2C_0322Context.Imgs.ToList();
            return result;
        }

        // GET api/<ImgController>/5
        [HttpGet("{id}")]
        public byte[] Get(int id)
        {
            var result = _B2C_0322Context.Imgs.Select(x => new Img
            {
                PID = x.PID,
                IID=x.IID,
                Img_path=x.Img_path,
                Img_name=x.Img_name
            }).Where(a=>a.PID==id).FirstOrDefault();

            byte[] getImg = System.IO.File.ReadAllBytes(result.Img_path+result.Img_name);
            //return new FileContentResult(getImg, "image/jpg");
            return getImg;
        }

        // POST api/<ImgController>
        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            if (files.Count==0)
            {
                return Ok(new { Msg = "空的啦", Status = "fail" });

            }
            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = "../B2CAPI/Img/" + file.FileName;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        //程式寫入的本地資料夾裡面
                        await file.CopyToAsync(stream);
                        
                    }
                    foreach (var item in files)
                    {
                        var img = new Img();
                        img.Img_name = item.FileName;
                        img.Img_path = "../B2CAPI/Img/";
                        img.status = "Y";
                        img.Btime = DateTime.Now;
                        img.Buser = 123;
                        await _B2C_0322Context.Imgs.AddAsync(img);

                    }
                    await _B2C_0322Context.SaveChangesAsync();
                }
                else {
                    return Ok(new { Msg = "失敗啦", Status = "fail" });
                }
            }
            //var data = _B2C_0322Context.Imgs;
            return Ok(new { Msg = "成功啦", Status = "success"});
        }

        // DELETE api/<ImgController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            var img = _B2C_0322Context.Imgs.Where(x => x.PID == id).ToList();
            _B2C_0322Context.Imgs.RemoveRange(img);
            await _B2C_0322Context.SaveChangesAsync();
            foreach (var item in img)
            {
                string path = item.Img_path + item.Img_name;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            return "success";
        }
    }
}
