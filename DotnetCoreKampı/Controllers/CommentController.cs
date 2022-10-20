using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;

namespace DotnetCoreKampı.Controllers
{
    public class CommentController : Controller
    {

        CommentManager commentManager;

        public CommentController(CommentManager commentManager)
        {
            this.commentManager = commentManager;
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = commentManager.GetById(id);
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
