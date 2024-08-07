using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUsersService : IGenericService<UserAdmin>
    {
        Task<UserAdmin> BL_ReadStringId(string userId);
    }
}
