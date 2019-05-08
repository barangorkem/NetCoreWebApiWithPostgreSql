using Microsoft.EntityFrameworkCore;
using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Data.Context;
using NetCorePostgreSql.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCorePostgreSql.Core.Repository
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext _context)
        {
            this._context = _context;
        }
        public void Delete(Products obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Products> GetAll()
        {
            
            return  _context.Products.Include("Users").ToList();
        }

        public Products GetById(int id)
        {
            return _context.Products.Include("Users").FirstOrDefault(x=>x.id==id);
        }

        public void Insert(Products obj)
        {

            _context.Products.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Products obj, Products obj2)
        {
            obj.Name = obj2.Name;
            obj.Price = obj2.Price;
            obj.UserId = obj2.UserId;
            _context.SaveChanges();
        }
    }
}
