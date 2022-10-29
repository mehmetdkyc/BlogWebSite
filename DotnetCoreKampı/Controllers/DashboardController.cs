using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DotnetCoreKampı.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
            this.writerManager = _writerManager;
            this.blogManager = _blogManager;
            this.categoryManager = _categoryManager;
            this.logger = logger;
        }

        public IActionResult Dashboard()
        {
            ViewBag.v1 = blogManager.GetList().Count;
            ViewBag.v2 = blogManager.GetList().Where(x => x.WriterID == GetWriterID()).Count();
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
            writerNew.WriterAbout = imageFile.WriterAbout;

            WriterValidator validationRules = new WriterValidator();
            ValidationResult result = validationRules.Validate(writerNew);
            if (result.IsValid)
            {
                if (imageFile.WriterImage != null)
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

            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                using (var img = Image.Load(image.OpenReadStream()))
                {
                    var newSize = ResizeImage(img, 90, 90);
                    string[] aSize = newSize.Split(',');
                    img.Mutate(h=>h.Resize(Convert.ToInt32(aSize[1]),Convert.ToInt32(aSize[0])));
                    img.Save(location);
                }
            }
            //var stream = new FileStream(location, FileMode.Create);
            //image.CopyTo(stream);
            return newImageName;
        }

        public static string ResizeImage(Image img, int w, int h)
        {
            if(img.Width> w || img.Height > h)
            {
                double wRatio = (double)img.Width / (double)w;
                double hRatio = (double)img.Height / (double)h;
                double ratio = Math.Max(wRatio, hRatio);
                int newWidth=(int)(img.Width / ratio);
                int newHeight=(int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }

        public int GetWriterID()
        {
            var writerId = HttpContext.Session.GetInt32("writerID");
            return writerId ?? 0;
        }

        [HttpGet]
        public IActionResult Password()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Password(string oldPassword, string newPassword)
        {
            try
            {
                var writerInfo = writerManager.GetById(GetWriterID());
                if (writerInfo != null && writerInfo.Password == oldPassword)
                {
                    writerInfo.Password = newPassword;
                    writerManager.TUpdate(writerInfo);
                    return Json(new { isSuccess = true, message = "" });
                }
                return Json(new { isSuccess = false, message = "Girdiğiniz şifre doğru değildir." });
            }
            catch (Exception ex)
            {

                return Json(new { isSuccess = false, message = ex.Message.ToString() });
            }


        }
    }
}
