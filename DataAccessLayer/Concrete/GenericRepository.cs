using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T, TContext> : IGenericRepository<T> where T : class, new() where TContext : DbContext
    {
        protected readonly TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }
        public async Task<List<T>> ListAsync(string[] includes, Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(filter).ToListAsync();
        }

        public async Task<List<T>> ListAsync(string city)
        {
            return await _context.Set<T>().Include(city).ToListAsync();
        }

        public async Task<T> ReadAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> GetImagesInAdverts(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetImagesInAdvertsAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> ReadStringIdAsync(string userId)
        {
            return await _context.Set<T>().FindAsync(userId);
        }

        public async Task<Contact> ReadInIPAddressAsync(Expression<Func<Contact, bool>> ipAddress)
        {
            return await _context.Set<Contact>().Where(ipAddress).OrderByDescending(m => m.MessageDate).FirstOrDefaultAsync();
        }
    }

}
