using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreKampı.ViewComponents.Comment
{
    public class CommentListByBlog : ViewComponent
    {
        CommentManager cm;

        public CommentListByBlog(CommentManager cm)
        {
            this.cm = cm;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = cm.GetList(id);
            return View(values);
        }
    }
}
