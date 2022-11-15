using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        BlogManager blogManager;

        public BlogController(BlogManager blogManager)
        {
            this.blogManager = blogManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var listValue = blogManager.GetBlogListWithCategory();
            return View(listValue);
        }
        [HttpGet]
        public IActionResult BlogDetail(int blogId)
        {
            var val = blogManager.GetBlogListWithCategory().Where(x=>x.BlogID==blogId).FirstOrDefault();
            return View(val);
        }
    }
}
