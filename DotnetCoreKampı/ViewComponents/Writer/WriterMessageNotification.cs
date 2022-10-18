using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Writer
{
    public class WriterMessageNotification: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
