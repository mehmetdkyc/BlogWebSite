using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult result = validationRules.Validate(category);

            if (result.IsValid)
            {
                category.CategoryStatus = true;
                category.CategoryCount = 0;
                cm.TAdd(category);

                return RedirectToAction("Index", "Category");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult EditCategory(Category category)
        {
            try
            {
                cm.TUpdate(category);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
            
        }

    }
}
