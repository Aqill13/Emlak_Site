using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdvertService : IGenericService<Advert>
    {
        Task<List<Advert>> BL_ListAsync(string[] includes, Expression<Func<Advert, bool>> filter);
    }
}
