using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetCoreKampı.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogManager blogManager;
        private readonly  CategoryManager cm;

        public BlogController(BlogManager blogManager, CategoryManager cm)
        {
            this.blogManager = blogManager;
            this.cm = cm;
        }

        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }
        
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.blogId = id;
            var values=blogManager.GetById(id);
            return View(values);
        }
        public IActionResult GetBlogsByWriterId()
        {

            var values = blogManager.GetBlogListWithCategory().Where(x => x.WriterID == GetWriterID()).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {


            var categoryValues = GetCategoryList();
            ViewBag.CategoryValues = categoryValues;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult result = validationRules.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogStatus = true;
                blog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterID = GetWriterID();
                blogManager.TAdd(blog);
                return RedirectToAction("GetBlogsByWriterId", "Blog");
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
        public JsonResult DeleteBlog(int id)
        {
            var blogValue = blogManager.GetById(id);
            blogManager.TDelete(blogValue);
            return Json(new { redirectToUrl = Url.Action("GetBlogsByWriterId", "Blog") });
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogValue = blogManager.GetById(id);
            var categoryValues = GetCategoryList();
            ViewBag.CategoryValues = categoryValues;
            return View(blogValue);
        }
        [HttpPost]
        public JsonResult EditBlog(Blog blog)
        {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult result = validationRules.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogStatus = true;
                blog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blogManager.TUpdate(blog);
                return Json(true);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return Json(false);
        }

        public List<SelectListItem> GetCategoryList()
        {
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            return categoryValues;
        }

        public int GetWriterID()
        {
            var writerMailAdress = HttpContext.Session.GetInt32("writerID");
            return writerMailAdress ?? 0;
        }
    }
}
