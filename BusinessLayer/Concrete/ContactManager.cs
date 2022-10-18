using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal contactDal;

        public ContactManager(IContactDal contactDal)
        {
            this.contactDal = contactDal;
        }

        public Contact GetById(int id)
        {
            return contactDal.GetTById(id);
        }

        public List<Contact> GetList()
        {
            return contactDal.GetAllList();
        }

        public void TAdd(Contact t)
        {
            contactDal.Insert(t);
        }

        public void TDelete(Contact t)
        {
            contactDal.Delete(t);
        }

        public void TUpdate(Contact t)
        {
            contactDal.Update(t);
        }
    }
}
