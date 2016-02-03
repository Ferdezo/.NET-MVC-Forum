using ForumData5;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.App_Start;

namespace WebApplication2.Controllers
{
    public class PostController : Controller
    {
        private ForumDAO forumDAO;
        private List<Post> posts;
        private static int topID;
        private static int editID;

        public PostController()
        {
            forumDAO = ForumDAO.GetInstance();
        }

        // GET: Post
        public ActionResult Index(int? id, int? page = 1, int pageSize = 5)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            topID = (int)id;
            posts = forumDAO.GetPosts((int)id);
            int pageNumber = (page ?? 1);
            return View(posts.OrderBy(p => p.posID).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Create(int? page = 1, int pageSize = 5)
        {
            string newContent = Request["newContent"];
            forumDAO.AddPost(newContent, topID, ApplicationState.user);
            posts = forumDAO.GetPosts(topID);
            return RedirectToAction("Index", new { id = topID });
        }

        public ActionResult Edit(int id, string content)
        {
            editID = id;
            ViewBag.Content = content;
            return View();
        }

        [HttpPost]
        public ActionResult EditSubmit(string content)
        {
            forumDAO.EditPostContent(editID, content);
            return RedirectToAction("Index", new { id = topID });
        }
    }
}