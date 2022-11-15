using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WritersController : Controller
    {
        WriterManager writerManager;

        public WritersController(WriterManager writerManager)
        {
            this.writerManager = writerManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var values = writerManager.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Profile(int id)
        {
            var idProfile = writerManager.GetWriterFullById(id);
            return View(idProfile);
        }
    }
}
