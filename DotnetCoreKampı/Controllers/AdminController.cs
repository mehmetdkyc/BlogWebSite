using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
