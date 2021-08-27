using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Core
{
    public class UserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value);
        }

        public string GetAuthToken()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        }
    }
}
