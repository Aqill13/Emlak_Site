using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class CityRepository : GenericRepository<City, Context>, ICityRepository
    {
        public CityRepository(Context context) : base(context)
        {
        }
    }
}
