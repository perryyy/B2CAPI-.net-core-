using B2CAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.service
{
    public class EditImg
    {
        private readonly B2C_0322Context _B2C_0322Context;
        public EditImg(B2C_0322Context Perry)
        {
            _B2C_0322Context = Perry;
        }
        public async Task<string> editImg(List<IFormFile> files, int PID)
        {
            //刪除 db_Img 資料
            var img = _B2C_0322Context.Imgs.Where(x => x.PID == PID).ToList();
             _B2C_0322Context.Imgs.RemoveRange(img);
            await _B2C_0322Context.SaveChangesAsync();
            //刪除 Img 實體位址
            foreach (var item in img)
            {
                string path = item.Img_path + item.Img_name;
                if (File.Exists(path)) 
                {
                    File.Delete(path);
                }
            }
            //新增 db_Img 資料
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
                    var imgtmp = new Img();
                    imgtmp.Img_name = file.FileName;
                    imgtmp.Img_path = "../B2CAPI/Img/";
                    imgtmp.status = "Y";
                    imgtmp.Btime = DateTime.Now;
                    imgtmp.Buser = 123;
                    imgtmp.PID = PID;
                    await _B2C_0322Context.Imgs.AddAsync(imgtmp);
                    await _B2C_0322Context.SaveChangesAsync();
                }
                else
                {
                    return "fail";
                }
            }
            _B2C_0322Context.SaveChanges();
            //新增 Img 實體位址
            return "success";
        }
    }
}
