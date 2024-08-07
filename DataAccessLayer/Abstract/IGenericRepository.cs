using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> ReadAsync(int id);
        Task<Contact> ReadInIPAddressAsync(Expression<Func<Contact, bool>> ipAddress);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(string city);
        Task<List<T>> ListAsync(string[] includes, Expression<Func<T, bool>> filter);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetImagesInAdvertsAsync(Expression<Func<T, bool>> filter);
        Task<T> ReadStringIdAsync(string userId);
    }
}
