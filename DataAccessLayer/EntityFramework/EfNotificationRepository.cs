﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfNotificationRepository:GenericRepository<Notification>, INotificationDal
    {
        private readonly Context _context;
        public EfNotificationRepository(Context context) : base(context)
        {
            this._context = context;
        }
    }
}
