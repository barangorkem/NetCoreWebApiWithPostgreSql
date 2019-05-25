using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Data.Models;
using NetCorePostgreSql.Service.DTO;
using NetCorePostgreSql.Service.DTOFunctions;

namespace NetCorePostgreSql.Service.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductsController(IProductRepository _productRepository, IHttpContextAccessor _httpContextAccessor, IConfiguration _config)
        {
            this._productRepository = _productRepository;
            this._httpContextAccessor = _httpContextAccessor;
            this._config = _config;
        }
        [HttpGet]
        public IActionResult GetProducts()


        {
            try
            {
                IEnumerable<Products> products = _productRepository.GetAll();
                return StatusCode((int)HttpStatusCode.OK, products.Select(x => new UserProducts()
                {
                    id = x.id,
                    Name = x.Name,
                    Price = x.Price,
                    UserName = x.Users.UserName
                }));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = "Hata oluştu" });
            }
        }
        [HttpPost]
        public IActionResult PostProduct(Products product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserFunctions userFunctions = new UserFunctions(this._httpContextAccessor);
                    product.UserId = userFunctions.GetUserId();
                    _productRepository.Insert(product);
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
        public IActionResult GetProduct(int id)
        {
            try
            {
                Products product = _productRepository.GetById(id);
                if (product != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, product);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new { mesage = "Bulunamadı." });
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { error = "Hata oluştu" });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                Products product = _productRepository.GetById(id);
                if (product != null)
                {
                    _productRepository.Delete(product);
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
        public IActionResult UpdateUser(int id, Products newProduct)
        {
            try
            {
                Products product = _productRepository.GetById(id);
                if (product != null)
                {
                    _productRepository.Update(product, newProduct);
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