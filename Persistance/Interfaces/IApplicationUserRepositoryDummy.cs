using Domain;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationUserRepositoryDummy
    {
        Task<ApplicationUser> GetUserAsync(string Username, string Password);
        Task<ApplicationUser> GetByCodeVerifier(string CodeVerifier);
        Task<bool> UpdateUserAsync(ApplicationUser applicationUser);
    }
}
