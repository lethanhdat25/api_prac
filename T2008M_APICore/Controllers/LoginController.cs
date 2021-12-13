using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using T2008M_APICore.Models;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;

namespace T2008M_APICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly T2008M_ASPContext _context;
        public IConfiguration _configuration;
        public LoginController(IConfiguration configuration, T2008M_ASPContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }


        [HttpPost]
        public async Task<IActionResult> Login(Account _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.PassWord != null)
            {
                //_userData.PassWord = GetMD5(_userData.PassWord);
                var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == _userData.Email && u.PassWord == _userData.PassWord);
                if (user != null)
                {
                    //tao token de tra ve
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["JWT:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("FullName",user.Name),
                        new Claim("Email",user.Email),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["JWT:Issuer"], _configuration["JWT:Audience"], claims,
                        expires: DateTime.Now.AddDays(1), signingCredentials: sign);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                return BadRequest("Email or Password isvalid!");
            }
            return BadRequest();
        }


    }
}
