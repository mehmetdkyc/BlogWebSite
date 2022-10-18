using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Writer
{
    public class WriterNotification: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
