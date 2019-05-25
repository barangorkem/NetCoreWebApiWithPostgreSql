using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCorePostgreSql.Data.Context;
using NetCorePostgreSql.Service.DTO;
using NetCorePostgreSql.Service.DTOFunctions;

namespace NetCorePostgreSql.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private IConfiguration _configuration;
        ApplicationDBContext _context;
        public JwtController(IConfiguration _configuration, ApplicationDBContext _context)
        {
            this._configuration = _configuration;
            this._context = _context;
        }

        [HttpPost]
        public IActionResult Post(LoginUser request)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);
                if (user == null)
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized, new { message = "Giriş bilgileriniz hatalıdır." });
                }
                RolesFunctions _rolesFunctions = new RolesFunctions(this._context);
                string[] userRoles = _rolesFunctions.GetUserRoles(user.id);
                List<Claim> claims = new List<Claim>
                {
            new Claim("UserName", user.UserName),
            new Claim("Email",user.Email),
            new Claim("Id",user.id.ToString()),
        };

                for (int i = 0; i < userRoles.Length; i++)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRoles[i]));
                }

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Issuer"], //appsettings.json içerisinde bulunan issuer değeri
                    audience: _configuration["Audience"],//appsettings.json içerisinde bulunan audince değeri
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60), // 30 gün geçerli olacak
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),//appsettings.json içerisinde bulunan signingkey değeri
                            SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), roles = userRoles });
            }
            return BadRequest();
        }
        [HttpGet]
        [Authorize]
        public LoginUser GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            LoginUser loginUser = new LoginUser()
            {
                UserName = identityClaims.FindFirst("UserName").Value,
                Email = identityClaims.FindFirst("Email").Value
            };
            return loginUser;
        }
    }
}