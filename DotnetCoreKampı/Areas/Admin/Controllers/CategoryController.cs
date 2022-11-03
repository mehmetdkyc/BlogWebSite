using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm;

        public CategoryController(CategoryManager cm)
        {
            this.cm = cm;
        }
        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }
    }
}
