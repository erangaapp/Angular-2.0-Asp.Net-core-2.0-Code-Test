using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MDB = CT.Models.Base;

namespace CT.Repo
{
    public interface IRepository<T> where T : MDB.BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Get(int id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        Task<T> InsertAsync(T entity);
        void SaveChangesAsync();

    }
}
