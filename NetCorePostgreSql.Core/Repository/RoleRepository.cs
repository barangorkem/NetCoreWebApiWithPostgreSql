using NetCorePostgreSql.Core.Infrastructure;
using NetCorePostgreSql.Data.Context;
using NetCorePostgreSql.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCorePostgreSql.Core.Repository
{
    public class RoleRepository : IRoleRepository
    {
        ApplicationDBContext _context;
        public RoleRepository(ApplicationDBContext _context)
        {
            this._context = _context;
        }
        public void Delete(Roles obj)
        {
            _context.Roles.Remove(obj);
            _context.SaveChanges();
        }

        public Roles FindByName(Roles obj)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleName == obj.RoleName);
        }

        public IEnumerable<Roles> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Roles GetById(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.id == id);
        }

        public void Insert(Roles obj)
        {
            _context.Roles.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Roles obj, Roles obj2)
        {
            obj.RoleName = obj2.RoleName;
            _context.SaveChanges();
        }
    }
}
