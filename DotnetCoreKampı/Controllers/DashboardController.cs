using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DotnetCoreKampı.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class DashboardController : Controller
    {
        WriterManager writerManager;
        BlogManager blogManager;
        CategoryManager categoryManager;
        private readonly ILogger<DashboardController> logger;
        
        public DashboardController(ILogger<DashboardController> logger, WriterManager _writerManager, BlogManager _blogManager, CategoryManager _categoryManager)
        {
            this.writerManager=_writerManager;
            this.blogManager = _blogManager;
            this.categoryManager = _categoryManager;
            this.logger = logger;
        }

        public IActionResult Dashboard()
        {
            ViewBag.v1 = blogManager.GetList().Count;
            ViewBag.v2 = blogManager.GetList().Where(x=>x.WriterID== GetWriterID()).Count();
            ViewBag.v3 = categoryManager.GetList().Count;
            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var values = writerManager.GetById(1);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateProfile(ProfilePictureModel imageFile)
        {
            var writerNew = writerManager.GetById(GetWriterID());
            writerNew.Name = imageFile.Name;
            writerNew.MailAdress = imageFile.MailAdress;
            writerNew.WriterAbout=imageFile.WriterAbout;

            WriterValidator validationRules = new WriterValidator();
            ValidationResult result = validationRules.Validate(writerNew);
            if (result.IsValid)
            {
                if(imageFile.WriterImage != null)
                {
                    writerNew.WriterImage = ImageAdd(imageFile.WriterImage);
                }
                writerManager.TUpdate(writerNew);
                return RedirectToAction("Dashboard", "Dashboard");
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
        public int GetWriterID()
        {
            var writerMailAdress = HttpContext.Session.GetInt32("writerID");
            return writerMailAdress ?? 0;
        }
    }
}
