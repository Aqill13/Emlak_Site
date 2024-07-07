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
    public class GeneralSettingsManager : IGeneralSettingsService
    {
        IGeneralSettingsRepository _repository;

        public GeneralSettingsManager(IGeneralSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task BL_CreateAsync(GeneralSettings entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(GeneralSettings entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<List<GeneralSettings>> BL_ListAsync()
        {
            return await _repository.ListAsync();

        }
        public async Task<List<GeneralSettings>> BL_ListAsync(Expression<Func<GeneralSettings, bool>> filter)
        {
            return await _repository.ListAsync(filter);
        }
        public async Task<GeneralSettings> BL_ReadAsync(int id)
        {
            return await _repository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(GeneralSettings entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
