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
    public class TypeManager : ITypeService
    {
        ITypeRepository _repository;

        public TypeManager(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task BL_CreateAsync(EntityLayer.Entities.Type entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(EntityLayer.Entities.Type entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<List<EntityLayer.Entities.Type>> BL_ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<List<EntityLayer.Entities.Type>> BL_ListAsync(Expression<Func<EntityLayer.Entities.Type, bool>> filter)
        {
            return await _repository.ListAsync(filter);
        }
        public async Task<EntityLayer.Entities.Type> BL_ReadAsync(int id)
        {
            return await _repository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(EntityLayer.Entities.Type entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
