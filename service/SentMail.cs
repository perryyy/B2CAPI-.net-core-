using B2CAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace B2CAPI.service
{
    public class SentMail
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly AddPassword _addPassword;
        public SentMail(B2C_0322Context Perry, AddPassword addPassword)
        {
            _B2C_0322Context = Perry;
            _addPassword = addPassword;
        }
        public void sentmail(Member result) {
            MailMessage mail = new MailMessage();
            string nowMail = Convert.ToString(result.email);
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            int passwordLength = 6;
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            string newpassword = new string(chars);
            result.password = _addPassword.HashPassword(newpassword);
            result.ctime = DateTime.Now;
            result.cuser = 123;
            _B2C_0322Context.SaveChanges();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress(nowMail, "Perry練習專案");

            //收信者email
            mail.To.Add(nowMail);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "密碼別再忘記囉";

            //內容
            mail.Body = "新密碼為" + newpassword + "請盡快更改";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("*********", "********");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

        }
    }
}
