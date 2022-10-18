using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorPage(int code)
        {
            return View();
        }
    }
}
