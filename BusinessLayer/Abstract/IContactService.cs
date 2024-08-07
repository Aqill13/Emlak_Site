using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContactService : IGenericService<Contact>
    {
        Task<Contact> ReadInIPAddress(Expression<Func<Contact, bool>> ipAddress);
    }
}
