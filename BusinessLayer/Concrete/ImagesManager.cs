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
    public class ImagesManager : IImagesService
    {
        IImagesRepository _repository;

        public ImagesManager(IImagesRepository repository)
        {
            _repository = repository;
        }
        public async Task BL_CreateAsync(Images entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(Images entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<List<Images>> BL_ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<List<Images>> BL_ListAsync(Expression<Func<Images, bool>> filter)
        {
            return await _repository.ListAsync(filter);
        }
        public async Task<Images> BL_ReadAsync(int id)
        {
            return await _repository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(Images entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<List<Images>> GetImagesInAdvertAsync(Expression<Func<Images, bool>> filter)
        {
            return await _repository.GetImagesInAdvertsAsync(filter);
        }
    }
}
