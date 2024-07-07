using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdvertManager : IAdvertService
    {
        IAdvertRepository _advertRepository;

        public AdvertManager(IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task BL_CreateAsync(Advert entity)
        {
            await _advertRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(Advert entity)
        {
            await _advertRepository.DeleteAsync(entity);
        }

        public async Task<List<Advert>> BL_ListAsync()
        {
            return await _advertRepository.ListAsync();
        }
        public async Task<List<Advert>> BL_ListAsync(Expression<Func<Advert, bool>> filter)
        {
            return await _advertRepository.ListAsync(filter);
        }

        public async Task<List<Advert>> BL_ListAsync(string[] includes, Expression<Func<Advert, bool>> filter)
        {
            return await _advertRepository.ListAsync(includes, filter);
        }

        public async Task<Advert> BL_ReadAsync(int id)
        {
            return await _advertRepository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(Advert entity)
        {
            await _advertRepository.UpdateAsync(entity);
        }
    }
}
