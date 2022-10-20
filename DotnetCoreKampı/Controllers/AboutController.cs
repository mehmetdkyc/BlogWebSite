using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager;

        public AboutController(AboutManager aboutManager)
        {
            this.aboutManager = aboutManager;
        }

        public IActionResult Index()
        {
            var values = aboutManager.GetList();
            return View(values);
        }
        public PartialViewResult SocialMediaAbout()
        {
            return PartialView();
        }
    }
}
