using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserAsync(string Username, string Password);
        Task<ApplicationUser> GetByCodeVerifier(string CodeVerifier);
        Task<bool> UpdateUserAsync(ApplicationUser applicationUser);
    }
}
