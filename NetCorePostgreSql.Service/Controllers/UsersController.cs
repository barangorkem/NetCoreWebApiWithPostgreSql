using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Data.Models;

namespace NetCorePostgreSql.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
           try
            {
                IEnumerable<Users> users = _userRepository.GetAll();
                return StatusCode((int)HttpStatusCode.OK,users);
            }
            catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = "Hata oluştu" });
            }
        }
        [HttpPost]
        public IActionResult PostUser(Users user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _userRepository.Insert(user);
                    return StatusCode((int)HttpStatusCode.Created, new { message = "Kayıt başarılıdır." });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { message = "Bir hata oluştu." });
                }
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = err });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
               Users user=_userRepository.GetById(id);
                if(user!=null)
                {
                    return StatusCode((int)HttpStatusCode.OK,user);

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { message = "Kayıt bulunamadı" });
                }
            }
            catch(Exception err)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = err });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                Users user = _userRepository.GetById(id);
                if (user != null)
                {
                    _userRepository.Delete(user);
                    return StatusCode((int)HttpStatusCode.OK, new { message = "Silme işlemi başarılıdır." });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { message = "Kayıt bulunamadı" });
                }
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = err });
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id,Users newUser)
        {
            try
            {
                Users user = _userRepository.GetById(id);
                if (user != null)
                {
                    _userRepository.Update(user,newUser);
                    return StatusCode((int)HttpStatusCode.OK, new { message = "Güncelleme işlemi başarılıdır." });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { message = "Kayıt bulunamadı" });
                }
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = err });
            }
        }
    }
}