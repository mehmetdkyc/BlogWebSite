using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        private readonly Context _context;
        public EfCommentRepository(Context context) : base(context)
        {
            this._context = context;
        }
        public List<Comment> GetCommentWithWriter(int id)
        {
            return _context.Comments.Include(x => x.Writer).Where(x => x.BlogID == id).ToList();
        }
    }
}
