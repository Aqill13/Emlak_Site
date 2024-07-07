using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task BL_CreateAsync(T entity);
        Task<T> BL_ReadAsync(int id);
        Task BL_UpdateAsync(T entity);
        Task BL_DeleteAsync(T entity);
        Task<List<T>> BL_ListAsync();
        Task<List<T>> BL_ListAsync(Expression<Func<T, bool>> filter);
    }
}
