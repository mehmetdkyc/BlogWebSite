using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm;

        public CategoryController(CategoryManager cm)
        {
            this.cm = cm;
        }

        public IActionResult Index()
        {
            var values= cm.GetList();

            return View(values);
        }
    }
}
