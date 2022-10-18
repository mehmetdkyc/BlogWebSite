using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotnetCoreKampı.Controllers
{
    public class LoginController : Controller
    {
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
            Context c=new Context();
            var dataValue = c.Writers.FirstOrDefault(x=>x.MailAdress == writer.MailAdress && x.Password == writer.Password);

            if(dataValue != null)
            {
                //HttpContext.Session.SetString("username", writer.MailAdress);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,writer.MailAdress)
                };
                var userIdentity = new ClaimsIdentity(claims,"a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Blog");
            }
            else
            {
                return View();
            }

            
        }
    }
}
