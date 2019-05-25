using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePostgreSql.Service.DTO
{
    public class UserProducts
    {
        public int id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string UserName { get; set; }
    }
}
