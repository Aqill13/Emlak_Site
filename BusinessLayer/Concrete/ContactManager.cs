using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        IContactRepository contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task BL_CreateAsync(Contact entity)
        {
            await contactRepository.CreateAsync(entity);
        }

        public async Task BL_DeleteAsync(Contact entity)
        {
            await contactRepository.DeleteAsync(entity);    
        }

        public async Task<List<Contact>> BL_ListAsync()
        {
            return await contactRepository.ListAsync();
        }

        public async Task<List<Contact>> BL_ListAsync(Expression<Func<Contact, bool>> filter)
        {
            return await contactRepository.ListAsync(filter);
        }

        public async Task<Contact> BL_ReadAsync(int id)
        {
            return await contactRepository.ReadAsync(id);
        }

        public async Task BL_UpdateAsync(Contact entity)
        {
            await contactRepository.UpdateAsync(entity);
        }

        public async Task<Contact> ReadInIPAddress(Expression<Func<Contact, bool>> ipAddress)
        {
            return await contactRepository.ReadInIPAddressAsync(ipAddress);
        }
    }
}
