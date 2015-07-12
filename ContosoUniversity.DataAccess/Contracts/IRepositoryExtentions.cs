using System;
using System.Linq;
using System.Linq.Expressions;

namespace ContosoUniversity.DataAccess.Contracts
{
    public static class IRepositoryExtentions
    {
        public static IQueryable<T> FindBy<T>(
            this IRepository<T> repository,
            Expression<Func<T, bool>> predicate) where T : class
        {
            return repository.Query(q => q.Where(predicate));
        }

        public static T GetSingle<T>(
            this IRepository<T> repository,
            Expression<Func<T, bool>> predicate) where T : class
        {
            return repository.Query(q => q.Where(predicate))
                             .SingleOrDefault();
        }
    }
}
