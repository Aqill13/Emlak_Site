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
    public class ImagesRepository : GenericRepository<Images, Context>, IImagesRepository
    {
        public ImagesRepository(Context context) : base(context)
        {
        }
    }
}
