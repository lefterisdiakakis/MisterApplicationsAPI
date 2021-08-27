using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistance
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
    }
}
