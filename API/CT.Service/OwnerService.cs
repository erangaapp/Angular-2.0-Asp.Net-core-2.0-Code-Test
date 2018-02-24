using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

using ABS = CT.Service.Abstract;
using MD = CT.Models;
using REPO = CT.Repo;
using System.Linq;

namespace CT.Service
{
    public class OwnerService : ABS.IOwnerService
    {
        private REPO.IRepository<MD.Owner> ownerRepository;

        public OwnerService(REPO.IRepository<MD.Owner> ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        public async Task<IEnumerable<MD.Owner>> GetAllAsync()
        {
            return await ownerRepository.GetAllAsync();
        }

        public async Task<MD.Owner> Get(int id)
        {
            return await ownerRepository.GetAsync(id);
        }

        public IEnumerable<MD.Owner> AllIncluding(params Expression<Func<MD.Owner, object>>[] includeProperties)
        {
            return ownerRepository.AllIncluding(includeProperties);
        }

        public async Task<IEnumerable<MD.Owner>> AllIncludingAsync(params Expression<Func<MD.Owner, object>>[] includeProperties)
        {
            return await ownerRepository.AllIncludingAsync(includeProperties);
        }

        public async Task<IEnumerable<MD.Owner>> GetByOwnersByPetTypeAsync(string petType)
        {
            return (await ownerRepository.AllIncludingAsync(x => x.Pets)).Where(w => w.Pets.Select(s => s.PetType.Type.ToLower()).Contains(petType.ToLower()));
        }

        public async Task<MD.Owner> Insert(MD.Owner entity)
        {
            await ownerRepository.InsertAsync(entity);
            return entity;

        }

        public void Update(MD.Owner entity)
        {
            ownerRepository.Update(entity);
        }

        public async void Delete(int id)
        {
            MD.Owner entity = await Get(id);
            ownerRepository.Delete(entity);
        }
      
    }
}
