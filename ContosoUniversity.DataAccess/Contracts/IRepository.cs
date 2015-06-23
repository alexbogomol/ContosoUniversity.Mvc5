﻿using System.Linq;

namespace ContosoUniversity.DataAccess.Contracts
{
    /// <summary>
    /// Simple generic repository contract
    /// </summary>
    /// <remarks>
    /// Taken from CodeCamper by John Papa:
    /// https://github.com/johnpapa/CodeCamper
    /// </remarks>
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
