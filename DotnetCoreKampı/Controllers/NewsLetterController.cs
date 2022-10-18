using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class NewsLetterController : Controller
    {
        NewsLetterManager newsLetterManager = new NewsLetterManager(new EfNewsLetterRepository());

        [HttpGet]
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult SubscribeMail(string mailAdress)         
        {
            if(mailAdress == null)
            {
                return Json(false);
            }
            NewsLetter newsLetter = new NewsLetter()
            {
                Mail = mailAdress,
                MailStatus = true
            };
            
            newsLetterManager.TAdd(newsLetter);
            return Json(true);
        }
    }
}
