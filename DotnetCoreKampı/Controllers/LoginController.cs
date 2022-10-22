using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotnetCoreKampı.Controllers
{
    public class LoginController : Controller
    {
        WriterManager _writer; 
        public LoginController(WriterManager writerManager)
        {
            this._writer = writerManager;
        }

        [AllowAnonymous]

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(Writer writer)
        {
            var dataValue = _writer.GetList().FirstOrDefault(x => x.MailAdress == writer.MailAdress && x.Password == writer.Password);

            if (dataValue != null)
            {
                var writerID = dataValue.ID;
                HttpContext.Session.SetInt32("writerID", writerID);
                HttpContext.Session.SetString("writerName", dataValue.Name);
                HttpContext.Session.SetString("writerPathUrl", dataValue.WriterImage);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,writer.MailAdress)
                };
                var userIdentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Blog");
            }
            else
            {
                return View();
            }


        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");

        }
    }
}
