using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using DoAnPhanMem.Common.Helpers;
using DoAnPhanMem.Models;
using PagedList;

namespace DoAnPhanMem.Areas.Admin.Controllers
{

    public class FeedbacksController : Controller, IFeedbacksController
    {
        private readonly WebshopEntities db = new WebshopEntities();
        private readonly IFeedbacksController feedbacksController;

        // GET: Areas/Brands
        public ActionResult Index(string search, int? size, int? page)
        {
            var pageSize = (size ?? 15);
            var pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = from a in db.Feedbacks
                       orderby a.create_at descending
                       select a;
            if (!string.IsNullOrEmpty(search))
            {
                list = from a in db.Feedbacks
                       where a.account_id.ToString().Contains(search)
                       orderby a.create_at descending
                       select a;
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public FeedbacksController()
        {
            feedbacksController = new UserAuthenticationDecorator(new FeedbacksController());
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
        {
            return feedbacksController.ReplyComment(comment, productID, reply_content, id);
        }
    }

}
