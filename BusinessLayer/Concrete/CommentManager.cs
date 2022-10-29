﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;


namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetTById(id);
        }

        public List<Comment> GetCommentWithWriter(int id)
        {
            return _commentDal.GetCommentWithWriter(id);
        }

        public List<Comment> GetList(int id)
        {
            return _commentDal.GetListAll(x=>x.BlogID==id);
        }

        public List<Comment> GetList()
        {
            return _commentDal.GetAllList();
        }

        public void TAdd(Comment t)
        {
            _commentDal.Insert(t);
        }

        public void TDelete(Comment t)
        {
            _commentDal.Delete(t);
        }

        public void TUpdate(Comment t)
        {
            _commentDal.Update(t);
        }
    }
}
