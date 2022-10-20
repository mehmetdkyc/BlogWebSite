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
            var values = writerManager.GetById(1);
            return View(values);
        }
    }
}
