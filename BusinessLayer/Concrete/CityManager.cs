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
    public class CityManager : ICityService
    {
        ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task BL_CreateAsync(City entity)
        {
            await _cityRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(City entity)
        {
            await _cityRepository.DeleteAsync(entity);
        }

        public async Task<List<City>> BL_ListAsync()
        {
            return await _cityRepository.ListAsync();
        }

        public async Task<List<City>> BL_ListAsync(Expression<Func<City, bool>> filter)
        {
            return await _cityRepository.ListAsync(filter);
        }
        public async Task<City> BL_ReadAsync(int id)
        {
            return await _cityRepository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(City entity)
        {
            await _cityRepository.UpdateAsync(entity);
        }

    }
}
