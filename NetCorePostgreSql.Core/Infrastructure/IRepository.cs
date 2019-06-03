using System;
using System.Collections.Generic;
using System.Text;

namespace NetCorePostgreSql.Core.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Insert(T obj);

        void Update(T obj, T obj2);

        void Delete(T obj);

        T FindByName(T obj);

    }
}
