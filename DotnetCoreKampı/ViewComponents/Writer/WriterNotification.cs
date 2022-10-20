using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Writer
{
    public class WriterNotification: ViewComponent
    {
        NotificationManager notificationManager;

        public WriterNotification(NotificationManager notificationManager)
        {
            this.notificationManager = notificationManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = notificationManager.GetList();
            return View(values);
        }
    }
}
