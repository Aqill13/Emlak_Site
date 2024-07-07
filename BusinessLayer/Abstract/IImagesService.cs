using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IImagesService : IGenericService<Images>
    {
        Task<List<Images>> GetImagesInAdvertAsync(Expression<Func<Images, bool>> filter);
    }
}
