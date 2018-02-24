using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

using ABS = CT.Service.Abstract;
using MD = CT.Models;
using REPO = CT.Repo;

namespace CT.Service
{
    public class PetService : ABS.IPetService
    {
        private REPO.IRepository<MD.Pet> petRepository;

        public PetService(REPO.IRepository<MD.Pet> petRepository)
        {
            this.petRepository = petRepository;
        }

        public async Task<IEnumerable<MD.Pet>> GetAllAsync()
        {
            return await petRepository.GetAllAsync();
        }

        public async Task<IEnumerable<MD.Pet>> GetPetsByPetTypeAsync(string petType)
        {
            return (await petRepository.AllIncludingAsync(x => x.Owner, x => x.PetType)).Where(w => w.PetType.Type.Trim().ToLower().Equals(petType.Trim().ToLower())).OrderBy(o => o.Owner.Gender);
        }

        public async Task<MD.Pet> Get(int id)
        {
            return await petRepository.GetAsync(id);
        }

        public IEnumerable<MD.Pet> AllIncluding(params Expression<Func<MD.Pet, object>>[] includeProperties)
        {
            return petRepository.AllIncluding(includeProperties);
        }

        public async Task<IEnumerable<MD.Pet>> AllIncludingAsync(params Expression<Func<MD.Pet, object>>[] includeProperties)
        {
            return await petRepository.AllIncludingAsync(includeProperties);
        }

        public async Task<MD.Pet> Insert(MD.Pet entity)
        {
            await petRepository.InsertAsync(entity);
            return entity;

        }

        public void Update(MD.Pet entity)
        {
            petRepository.Update(entity);
        }

        public async void Delete(int id)
        {
            MD.Pet entity = await Get(id);
            petRepository.Delete(entity);
        }

    }
}
