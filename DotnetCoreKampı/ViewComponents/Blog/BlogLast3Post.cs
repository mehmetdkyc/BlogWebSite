using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Blog
{
    public class BlogLast3Post:ViewComponent
    {
        BlogManager blogManager;

        public BlogLast3Post(BlogManager blogManager)
        {
            this.blogManager = blogManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetLast3Blog();
            return View(values);
        }
    }
}
