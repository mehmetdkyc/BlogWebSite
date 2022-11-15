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
    public class EfWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        private readonly Context _context;
        public EfWriterRepository(Context context) : base(context)
        {
            this._context = context;
        }

        public Writer GetWriterFullById(int id)
        {
            return _context.Writers.Include(x => x.Blogs).ThenInclude(x => x.Comments).Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
