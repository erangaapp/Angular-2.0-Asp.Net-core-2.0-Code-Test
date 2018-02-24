using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MD = CT.Models;

namespace CT.Service.Abstract
{
    public interface IPetService
    {
        Task<IEnumerable<MD.Pet>> GetAllAsync();
        Task<IEnumerable<MD.Pet>> GetPetsByPetTypeAsync(string petType);
        Task<MD.Pet> Get(int id);
        IEnumerable<MD.Pet> AllIncluding(params Expression<Func<MD.Pet, object>>[] includeProperties);
        Task<IEnumerable<MD.Pet>> AllIncludingAsync(params Expression<Func<MD.Pet, object>>[] includeProperties);
        Task<MD.Pet> Insert(MD.Pet entity);
        void Update(MD.Pet entity);
        void Delete(int id);
    }
}
