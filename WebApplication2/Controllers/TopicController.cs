using ForumData5;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebApplication2.App_Start;

namespace WebApplication2.Controllers
{
    public class TopicController : Controller
    {
        private ForumDAO forumDAO;
        private List<Topic> topics;
        private static int editID;

        public TopicController()
        {
            forumDAO = ForumDAO.GetInstance();
        }

        // GET: Topic
        public ActionResult Index(int? page = 1, int pageSize = 5)
        {
            //ViewBag.Topics = forumDAO.GetTopics();
            topics = forumDAO.GetTopics();
            int pageNumber = (page ?? 1);
            return View(topics.OrderBy(t => t.topID).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Create(int? page = 1, int pageSize = 5)
        {
            string newTitle = Request["newTitle"];
            forumDAO.AddTopic(newTitle, ApplicationState.user);
            return RedirectToAction("Index", "Topic");
        }

        public ActionResult Edit(int id, string title)
        {
            editID = id;
            ViewBag.Title = title;
            return View();
        }

        [HttpPost]
        public ActionResult EditSubmit(string title)
        {
            forumDAO.EditTopicTitle(editID, title);
            return RedirectToAction("Index", "Topic");
        }

    }
}