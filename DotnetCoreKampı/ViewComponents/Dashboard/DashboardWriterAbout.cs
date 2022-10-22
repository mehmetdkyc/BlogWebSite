using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Dashboard
{
    public class DashboardWriterAbout : ViewComponent
    {
        WriterManager writerManager;

        public DashboardWriterAbout(WriterManager writerManager)
        {
            this.writerManager = writerManager;
        }

        public IViewComponentResult Invoke()
        {
            var id = HttpContext.Session.GetInt32("writerID");
            var values = writerManager.GetById(id ?? 0);
            return View(values);
        }
    }
}
