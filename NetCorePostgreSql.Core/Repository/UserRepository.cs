using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Data.Context;
using NetCorePostgreSql.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCorePostgreSql.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext _context)
        {
            this._context = _context;
        }
        public void Delete(Users obj)
        {
            try
            {
                _context.Users.Remove(obj);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
               
        }

        public IEnumerable<Users> GetAll()
        {
           return _context.Users.ToList();
        }

        public Users GetById(int id)
        {
            Users user = _context.Users.FirstOrDefault(x => x.id == id);
            return user;
           
        }

        public void Insert(Users obj)
        {
           
                _context.Users.Add(obj);
                _context.SaveChanges();
            
        }

        public void Update(Users obj, Users obj2)
        {
            obj.Email = obj2.Email;
            obj.Password = obj2.Password;
            obj.UserName = obj2.UserName;
            _context.SaveChanges();
        }

        public Users FindByName(Users findUser)
        {
            Users user = _context.Users.FirstOrDefault(x => x.Email == findUser.Email || x.UserName == findUser.UserName);
            return user;
        }
    }
}
