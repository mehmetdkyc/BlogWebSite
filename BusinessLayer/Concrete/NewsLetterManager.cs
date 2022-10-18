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
    public class NewsLetterManager : INewsLetterService
    {
        INewsLetterDal _newsLetter;

        public NewsLetterManager(INewsLetterDal newsLetter)
        {
            _newsLetter = newsLetter;
        }

        public NewsLetter GetById(int id)
        {
            return _newsLetter.GetTById(id);
        }

        public List<NewsLetter> GetList()
        {
            return _newsLetter.GetAllList();
        }

        public void TAdd(NewsLetter t)
        {
            _newsLetter.Insert(t);
        }

        public void TDelete(NewsLetter t)
        {
            _newsLetter.Delete(t);
        }

        public void TUpdate(NewsLetter t)
        {
            _newsLetter.Update(t);
        }
    }
}
