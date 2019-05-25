using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCorePostgreSql.Service.DTOFunctions
{
    public class UserFunctions
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ClaimsIdentity identityClaims;
        public UserFunctions(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {


            identityClaims = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            return Convert.ToInt32(identityClaims.FindFirst("Id").Value);
        }


    }
}
