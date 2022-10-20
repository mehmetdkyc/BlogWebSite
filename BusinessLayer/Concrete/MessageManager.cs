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
    public class MessageManager : IMessageService
    {
        IMessageDal messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            this.messageDal = messageDal;
        }

        public Message GetById(int id)
        {
            return messageDal.GetTById(id);
        }

        public List<Message> GetList()
        {
            return messageDal.GetAllList();
        }

        public List<Message> GetInboxMessagesByWriter(string mailAdress)
        {
            return messageDal.GetListAll(x=>x.Receiver==mailAdress);
        }

        public void TAdd(Message t)
        {
            messageDal.Insert(t);
        }

        public void TDelete(Message t)
        {
            messageDal.Delete(t);
        }

        public void TUpdate(Message t)
        {
            messageDal.Update(t);
        }
    }
}
