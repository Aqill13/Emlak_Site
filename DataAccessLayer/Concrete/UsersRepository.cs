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
    public class UsersRepository : GenericRepository<UserAdmin, Context>, IUsersRepository
    {
        public UsersRepository(Context context) : base(context)
        {
        }
    }
}
