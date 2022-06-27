using B2CAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace B2CAPI.service
{
    public class JWT
    {
        private readonly B2C_0322Context _B2C_0322Context;
        private readonly IConfiguration _configuration;
        private readonly AddPassword _addPassword;
        public JWT(B2C_0322Context Perry, IConfiguration Configuration, AddPassword addPassword)
        {
            _B2C_0322Context = Perry;
            _configuration = Configuration;
            _addPassword = addPassword;
        }
        //產生JWT
        public string MakeJWT(Member value)
        {
            var user = (from a in _B2C_0322Context.Members
                        where a.username == value.username
                        && a.password == value.password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return "帳號密碼錯誤";
            }
            else
            {
                //設定使用者資訊
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.username),
                    new Claim("FullName", user.username),
                    new Claim(JwtRegisteredClaimNames.NameId, user.MID.ToString())
                };

                var role = from a in _B2C_0322Context.Members
                           where a.MID == user.MID
                           select a;
                //設定Role
                foreach (var temp in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, temp.username));
                }

                //取出appsettings.json裡的KEY處理
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));

                //設定jwt相關資訊
                var jwt = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

                //產生JWT Token
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                //回傳JWT Token給認證通過的使用者
                return token;
            }
        }
    }
}
