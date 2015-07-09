using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface IAsyncRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
