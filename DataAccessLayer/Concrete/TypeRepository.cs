using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class TypeRepository : GenericRepository<EntityLayer.Entities.Type, Context>, ITypeRepository
    {
        public TypeRepository(Context context) : base(context)
        {
        }
    }
}
