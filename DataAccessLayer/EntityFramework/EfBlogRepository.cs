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
    public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        private readonly Context _context;
        public EfBlogRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public List<Blog> GetListWithCategory()
        {
            return _context.Blogs.Include(c => c.Category).Include(c=>c.Comments).ToList();
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {

            return _context.Blogs.Include(c => c.Category).Where(x => x.WriterID == id).ToList();

        }

    }
}
