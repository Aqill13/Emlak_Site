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
    public class ContactRepository : GenericRepository<Contact, Context>, IContactRepository
    {
        public ContactRepository(Context context) : base(context)
        {
        }
    }
}
