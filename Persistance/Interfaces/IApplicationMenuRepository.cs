using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationMenuRepository
    {
        Task<List<ApplicationMenu>> FindAll(int UserId,int AppId,string Langunange);
    }
}
