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
    public class AboutManager : IAboutService
    {
        IAboutDal aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            this.aboutDal = aboutDal;
        }

        public About GetById(int id)
        {
            return aboutDal.GetTById(id);
        }

        public List<About> GetList()
        {
            return aboutDal.GetAllList();
        }

        public void TAdd(About t)
        {
            aboutDal.Insert(t);
        }

        public void TDelete(About t)
        {
            aboutDal.Delete(t);
        }

        public void TUpdate(About t)
        {
            aboutDal.Update(t);
        }
    }
}
