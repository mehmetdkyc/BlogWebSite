using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager;

        public ContactController(ContactManager contactManager)
        {
            this.contactManager = contactManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact p)
        {
            p.ContactDate = DateTime.Now;
            p.ContactStatus = true;
            contactManager.TAdd(p);
            return RedirectToAction("Index","Blog");
        }
    }
}
