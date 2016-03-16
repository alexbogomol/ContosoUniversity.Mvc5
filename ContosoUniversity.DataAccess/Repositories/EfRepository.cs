using ContosoUniversity.DataAccess.Contracts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <remarks>
    /// Taken from CodeCamper by John Papa:
    /// https://github.com/johnpapa/CodeCamper
    /// </remarks>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class EfRepository<T> : IRepository<T> where T : class
    {
        public EfRepository(DbContext dbContext)
        {
            if (dbContext == null) 
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return Query(query => query);
        }

        public virtual T GetById(int id)
        {
            return Query(query => DbSet.Find(id));
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Remove(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Remove(entity);
        }

        public IQueryable<T> Query(Func<IQueryable<T>, IQueryable<T>> query)
        {
            return query(DbSet);
        }

        public TResult Query<TResult>(Func<IQueryable<T>, TResult> query)
        {
            return query(DbSet);
        }
    }
}
