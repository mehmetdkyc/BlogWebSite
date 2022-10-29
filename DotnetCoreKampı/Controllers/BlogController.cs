using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DotnetCoreKampı.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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

            var values = blogManager.GetBlogListWithCategory().Where(x => x.BlogID == id).SingleOrDefault();
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
        public IActionResult BlogAdd(BlogPictureModel blog)
        {
            Blog newBlog = new Blog();
            newBlog.BlogContent = blog.BlogContent;
            newBlog.CategoryID = blog.CategoryID;
            newBlog.BlogTitle = blog.BlogTitle;
            newBlog.BlogThumbnailImage = ImageAdd(blog.BlogThumbnailImage);
            newBlog.BlogImage = ImageAdd(blog.BlogImage);

            BlogValidator validationRules = new BlogValidator();
            ValidationResult result = validationRules.Validate(newBlog);

            if (result.IsValid)
            {
                newBlog.BlogStatus = true;
                newBlog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                newBlog.WriterID = GetWriterID();
                blogManager.TAdd(newBlog);

                var increaseCount = cm.GetById(newBlog.CategoryID);
                increaseCount.CategoryCount += 1;
                cm.TUpdate(increaseCount);

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

        public static string ImageAdd(IFormFile image)
        {

            var extension = Path.GetExtension(image.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogPostImage/",
                newImageName);

            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                using (var img = Image.Load(image.OpenReadStream()))
                {
                    var newSize = ResizeImage(img, 420, 380);
                    string[] aSize = newSize.Split(',');
                    img.Mutate(h => h.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                    img.Save(location);
                }
            }
            return newImageName;
        }

        public static string ResizeImage(Image img, int w, int h)
        {
            if (img.Width > w || img.Height > h)
            {
                double wRatio = (double)img.Width / (double)w;
                double hRatio = (double)img.Height / (double)h;
                double ratio = Math.Max(wRatio, hRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }



    }
}
