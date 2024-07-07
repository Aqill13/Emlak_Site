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
    public class DistrictManager : IDistrictService
    {
        IDistrictRepository _districtRepository;

        public DistrictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task BL_CreateAsync(District entity)
        {
            await _districtRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(District entity)
        {
            await _districtRepository.DeleteAsync(entity);
        }

        public async Task<List<District>> BL_ListAsync()
        {
            return await _districtRepository.ListAsync();
        }

        public async Task<List<District>> BL_ListAsync(Expression<Func<District, bool>> filter)
        {
            return await _districtRepository.ListAsync(filter);
        }

        public async Task<List<District>> BL_ListAsync(string city)
        {
            return await _districtRepository.ListAsync(city);
        }

        public async Task<District> BL_ReadAsync(int id)
        {
            return await _districtRepository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(District entity)
        {
            await _districtRepository.UpdateAsync(entity);
        }
    }
}
