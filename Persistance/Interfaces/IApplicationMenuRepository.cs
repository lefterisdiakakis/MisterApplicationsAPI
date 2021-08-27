using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationMenuRepository
    {
        Task<List<ApplicationMenu>> FindAll(int UserId,int AppId,string Langunange);
    }
}
