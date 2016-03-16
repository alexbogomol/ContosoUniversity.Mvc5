using System;
using System.Linq;

namespace ContosoUniversity.DataAccess.Contracts
{
    /// <summary>
    /// Simple generic repository contract
    /// </remarks>
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Remove(T entity);
        void Remove(int id);

        IQueryable<T> Query(Func<IQueryable<T>, IQueryable<T>> query);
        TResult Query<TResult>(Func<IQueryable<T>, TResult> query);
    }
}
