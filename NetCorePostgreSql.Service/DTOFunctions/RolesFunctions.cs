using NetCorePostgreSql.Data.Context;
using System.Linq;


namespace NetCorePostgreSql.Service.DTOFunctions
{
    public class RolesFunctions
    {

        ApplicationDBContext _context;
        public RolesFunctions(ApplicationDBContext _context)
        {
            this._context = _context;
        }
        public string[] GetUserRoles(int userId)
        {
            string[] roles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.Roles.RoleName).ToArray();
            return roles;
        }
    }
}
