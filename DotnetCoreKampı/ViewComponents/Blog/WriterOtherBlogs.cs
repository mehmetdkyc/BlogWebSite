using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Blog
{
    public class WriterOtherBlogs : ViewComponent
    {
        BlogManager blogManager;

        public WriterOtherBlogs(BlogManager blogManager)
        {
            this.blogManager = blogManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetBlogListWithWriter(1);
            return View(values);
        }
    }
}
