using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Dashboard
{
    public class DashboardCategoryList: ViewComponent
    {
        CategoryManager cm;

        public DashboardCategoryList(CategoryManager cm)
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
