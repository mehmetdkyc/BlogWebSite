using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class DashboardController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        public IActionResult Dashboard()
        {
            Context context = new Context();
            ViewBag.v1 = context.Blogs.ToList().Count;
            ViewBag.v2 = context.Blogs.Where(x=>x.WriterID==1).Count();
            ViewBag.v3 = context.Categories.ToList().Count;
            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var values = writerManager.GetById(1);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateProfile(Writer writer)
        {
            WriterValidator validationRules = new WriterValidator();
            ValidationResult result = validationRules.Validate(writer);
            if (result.IsValid)
            {
                writer.Status = true;
                if(writer.WriterImage != null)
                {
                    //writer.WriterImage = ImageAdd(writer.WriterImage);
                }
                writerManager.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
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
        public static string ImageAdd(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFile/",
                newImageName);
            var stream = new FileStream(location, FileMode.Create);
            image.CopyTo(stream);
            return newImageName;
        }
    }
}
