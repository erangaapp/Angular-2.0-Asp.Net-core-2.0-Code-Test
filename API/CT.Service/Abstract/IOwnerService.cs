using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MD = CT.Models;

namespace CT.Service.Abstract
{
    public interface IOwnerService
    {
        Task<IEnumerable<MD.Owner>> GetAllAsync();
        Task<MD.Owner> Get(int id);
        IEnumerable<MD.Owner> AllIncluding(params Expression<Func<MD.Owner, object>>[] includeProperties);
        Task<IEnumerable<MD.Owner>> AllIncludingAsync(params Expression<Func<MD.Owner, object>>[] includeProperties);
        Task<MD.Owner> Insert(MD.Owner entity);
        void Update(MD.Owner entity);
        void Delete(int id);
        Task<IEnumerable<MD.Owner>> GetByOwnersByPetTypeAsync(string petType);
    }
}
