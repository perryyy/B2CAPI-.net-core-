using B2CAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.service
{
    public class UploadImg
    {
        private readonly B2C_0322Context _B2C_0322Context;
        public UploadImg(B2C_0322Context Perry)
        {
            _B2C_0322Context = Perry;
        }
        public async Task<string> upload(List<IFormFile> files,int PID)
        {
            if (files.Count == 0)
            {
                return "fail";
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
                        var img = new Img();
                        img.Img_name = file.FileName;
                        img.Img_path = "../B2CAPI/Img/";
                        img.status = "Y";
                        img.Btime = DateTime.Now;
                        img.Buser = 123;
                        img.PID = PID;
                        await _B2C_0322Context.Imgs.AddAsync(img);
                    await _B2C_0322Context.SaveChangesAsync();
                }
                else
                {
                    return  "fail" ;
                }
            }
            _B2C_0322Context.SaveChanges();
            return "success";
        }
    }
}
