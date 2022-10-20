using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Category
{
    public class CategoryList: ViewComponent
    {
        CategoryManager cm;

        public CategoryList(CategoryManager cm)
        {
            this.cm = cm;
        }

        public IViewComponentResult Invoke()
        {
            var values = cm.GetList();
            return View(values);
        }
    }
}
