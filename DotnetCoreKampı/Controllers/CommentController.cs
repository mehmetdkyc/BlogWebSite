using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace DotnetCoreKampı.Controllers
{
    public class CommentController : Controller
    {

        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = commentManager.GetList(id);
            return PartialView(values);
        }
        [HttpGet]
        public PartialViewResult PartialAddComment(int id)
        {
            ViewBag.blogId = id;
            return PartialView();
        }
        [HttpPost]
        public IActionResult PartialAddComment(Comment comment)
        {
            comment.Date =DateTime.Now;
            comment.CommentStatus = true;
            commentManager.TAdd(comment);

            return RedirectToAction("BlogReadAll", "Blog", new { id =comment.BlogID});
        }
    }
}
