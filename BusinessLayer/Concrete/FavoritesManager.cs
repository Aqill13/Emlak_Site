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
    public class FavoritesManager : IFavoritesService
    {
        private readonly IFavoritesRepository _favoriteRepository;

        public FavoritesManager(IFavoritesRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task BL_CreateAsync(Favorites entity)
        {
            await _favoriteRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(Favorites entity)
        {
            await _favoriteRepository.DeleteAsync(entity);
        }

        public async Task<List<Favorites>> BL_ListAsync()
        {
            return await _favoriteRepository.ListAsync();
        }

        public async Task<List<Favorites>> BL_ListAsync(Expression<Func<Favorites, bool>> filter)
        {
            return await _favoriteRepository.ListAsync(filter);
        }

        public async Task<Favorites> BL_ReadAsync(int id)
        {
            return await _favoriteRepository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(Favorites entity)
        {
            await _favoriteRepository.UpdateAsync(entity); 
        }
    }
}
