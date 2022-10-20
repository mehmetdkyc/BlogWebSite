using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Dashboard
{
    public class DashboardLast5Posts: ViewComponent
    {
        BlogManager blogManager;

        public DashboardLast5Posts(BlogManager blogManager)
        {
            this.blogManager = blogManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetBlogListWithCategory().Take(5).ToList();
            return View(values);
        }
    }
}
