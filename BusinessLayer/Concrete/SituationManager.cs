using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SituationManager : ISituationService
    {
        ISituationRepository _repository;

        public SituationManager(ISituationRepository repository)
        {
            _repository = repository;
        }

        public async Task BL_CreateAsync(Situation entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(Situation entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<List<Situation>> BL_ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<List<Situation>> BL_ListAsync(Expression<Func<Situation, bool>> filter)
        {
            return await _repository.ListAsync(filter);
        }
        public async Task<Situation> BL_ReadAsync(int id)
        {
            return await _repository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(Situation entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
