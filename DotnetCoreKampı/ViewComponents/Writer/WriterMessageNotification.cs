using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Writer
{
    public class WriterMessageNotification: ViewComponent
    {
        MessageManager messageManager;

        public WriterMessageNotification(MessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        public IViewComponentResult Invoke()
        {
            string p = "mehmet@gmail.com";
            var values = messageManager.GetInboxMessagesByWriter(p);
            return View(values);
        }
    }
}
