using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DoAnPhanMem.Models
{
    //component
    public interface IFeedbacksController
    {
        JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id);
    }

    //concrete component
    public class FeedbacksConcrete : Controller, IFeedbacksController
    {
        private readonly WebshopEntities db = new WebshopEntities();

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
        {
            var user = Session["TaiKhoan"] as Account;
            bool result = false;
            if (user != null && user.role_id == 1)
            {
                int userID = user.acc_id;
                comment.account_id = userID;
                comment.product_id = productID;
                comment.content = reply_content;
                comment.replyfor = id;
                comment.status = "2";
                comment.create_at = DateTime.Now;

                db.Feedbacks.Add(comment);
                db.SaveChanges();

                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }

    //decorator
    public abstract class AuthenticationDecorator : IFeedbacksController
    {
        protected IFeedbacksController _feedbacksController;

        public AuthenticationDecorator(IFeedbacksController feedbacksController)
        {
            _feedbacksController = feedbacksController;
        }


        public virtual JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
        {
            var user = HttpContext.Current.Session["TaiKhoan"] as Account;
            if (user != null && user.role_id == 1)
            {
                return _feedbacksController.ReplyComment(comment, productID, reply_content, id);
            }
            else
            {
                //trả về đối tượng JsonResult chứa dữ liệu JSON với giá trị false
                return new JsonResult
                {
                    Data = false,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }

    //concrete decorator
    public class UserAuthenticationDecorator : AuthenticationDecorator
    {
        public UserAuthenticationDecorator(IFeedbacksController feedbacksController) : base(feedbacksController) { }

        public override JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
        {
            return base.ReplyComment(comment, productID, reply_content, id);
        }
    }
}
