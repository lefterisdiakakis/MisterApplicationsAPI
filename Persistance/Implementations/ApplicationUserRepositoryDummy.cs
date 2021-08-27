using Domain;
using Persistance.Interfaces;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class ApplicationUserRepositoryDummy : IApplicationUserRepository
    {
        private static ApplicationUser applicationUser = new ApplicationUser { Id = 1, Username = "Lefteris", Password = "Test" };

        public Task<ApplicationUser> GetByCodeVerifier(string CodeVerifier)
        {
            if (applicationUser.Code_Verifier == CodeVerifier)
            {
                return Task.FromResult( applicationUser);
            }
            else
            {
                return Task.FromResult((ApplicationUser)null);
            }
        }

        public Task<ApplicationUser> GetUserAsync(string Username, string Password)
        {
            return Task.FromResult(applicationUser);
        }

        public Task<bool> UpdateUserAsync(ApplicationUser newApplicationUser)
        {
            applicationUser.Code_Verifier = newApplicationUser.Code_Verifier;
            return Task.FromResult(true);
        }
    }
}
