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

        public IActionResult ReloadEvents(int id)
        {
            return ViewComponent("CommentListByBlog", new {id=id});
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
            try
            {
                comment.Date = DateTime.Now;
                comment.CommentStatus = true;
                comment.WriterID = GetWriterID();
                comment.CommentUserName= HttpContext.Session.GetString("writerName");
                commentManager.TAdd(comment);
                return RedirectToAction("ReloadEvents", "Comment", new { id = comment.BlogID });
            }
            catch (Exception)
            {

                return View();
            }
        }
        public int GetWriterID()
        {
            var writerMailAdress = HttpContext.Session.GetInt32("writerID");
            return writerMailAdress ?? 0;
        }
    }
}
