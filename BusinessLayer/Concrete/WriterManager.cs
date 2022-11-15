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
    public class WriterManager : IWriterService
    {
        IWriterDal writerDal;
        public WriterManager(IWriterDal writerDal)
        {
            this.writerDal = writerDal;
        }

        public Writer GetById(int id)
        {
            return writerDal.GetTById(id);
        }

        public List<Writer> GetList()
        {
            return writerDal.GetAllList();
        }

        public Writer GetWriterFullById(int id)
        {
            return writerDal.GetWriterFullById(id);
        }

        public void TAdd(Writer t)
        {
            writerDal.Insert(t);
        }

        public void TDelete(Writer t)
        {
            writerDal.Delete(t);
        }

        public void TUpdate(Writer t)
        {
            writerDal.Update(t);
        }

    }
}
