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
    public class UsersManager : IUsersService
    {
        IUsersRepository _usersRepository;

        public UsersManager(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task BL_CreateAsync(UserAdmin entity)
        {
            await _usersRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(UserAdmin entity)
        {
            await _usersRepository.DeleteAsync(entity);
        }

        public async Task<List<UserAdmin>> BL_ListAsync()
        {
            return await _usersRepository.ListAsync();
        }

        public async Task<List<UserAdmin>> BL_ListAsync(Expression<Func<UserAdmin, bool>> filter)
        {
            return await _usersRepository.ListAsync(filter);
        }

        public async Task<UserAdmin> BL_ReadAsync(int id)
        {
            return await _usersRepository.ReadAsync(id);
        }

        public async Task<UserAdmin> BL_ReadStringId(string userId)
        {
            return await _usersRepository.ReadStringIdAsync(userId);
        }

        public async Task BL_UpdateAsync(UserAdmin entity)
        {
            await _usersRepository.UpdateAsync(entity);
        }
    }
}
