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
    public class DistrictRepository : GenericRepository<District, Context>, IDistrictRepository
    {
        public DistrictRepository(Context context) : base(context)
        {
        }
    }
}
