using DoAnPhanMem.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PagedList;

namespace DoAnPhanMem.Controllers
{

    public abstract class ProductControllerBase : Controller
    {
        protected WebshopEntities db = new WebshopEntities();


        protected IPagedList GetProduct(Expression<Func<Product, bool>> expr, int? page)
        {
            int pageSize = 9; //số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); //trang hiện tại

            ViewBag.ListCate = db.Categories.ToList();
            ViewBag.ListBrand = db.Brands.ToList();
            ViewBag.AvgFeedback = db.Feedbacks.ToList();
            ViewBag.OrderDetail = db.Oder_Detail.ToList();

            var list = db.Products.Where(expr).OrderByDescending(m => m.pro_id).ToPagedList(pageNumber, pageSize);

            ViewBag.Showing = list.Count();
            return list;
        }


        protected virtual Product GetProductDetail(int id)
        {
            return db.Products.SingleOrDefault(m => m.status_ == "1" && m.pro_id == id);
        }

        protected virtual void SetRelatedProduct(Product product)
        {
            ViewBag.relatedproduct = db.Products.Where(item => item.status_ == "1" && item.pro_id != product.pro_id).Take(8).ToList();
        }

        protected virtual void SetProductImage(int id)
        {
            ViewBag.ProductImage = db.ProductImgs.Where(item => item.product_id == id).ToList();
        }

        protected virtual void SetFeedbacks()
        {
            ViewBag.ListFeedback = db.Feedbacks.Where(m => m.status == "2").ToList();
        }

        protected virtual void SetOrderFeedbacks()
        {
            ViewBag.OrderFeedback = db.Oder_Detail.ToList();
        }

        protected virtual void SetComment(int currentPage, int pageSize, Product product)
        {
            var comments = db.Feedbacks.Where(m => m.product_id == product.pro_id && m.status == "2").OrderByDescending(m => m.create_at).ToList();

            ViewBag.CountFeedback = comments.Count();
            ViewBag.PagerFeedback = comments.ToPagedList(currentPage, pageSize);
        }

        public virtual JsonResult HandleCommentAction(Feedback comment, int productID, int rateStar, string commentContent)
        {
            var user = Session["TaiKhoan"] as Account;
            bool result = false;

            if (user != null)
            {
                int userID = user.acc_id;
                comment.account_id = userID;
                comment.rate_star = rateStar;
                comment.product_id = productID;
                comment.content = commentContent;
                comment.status = "2";
                comment.create_at = DateTime.Now;
                bool hasPurchased = db.Oder_Detail.Any(od => od.Order.acc_id == userID && od.pro_id == productID && od.Order.status == "3");
                if (hasPurchased || user.Role.role_name == "Admin" || user.Role.role_name == "Nhân viên")
                {
                    db.Feedbacks.Add(comment);
                    db.SaveChanges();
                    result = true;
                    Notification.setNotification3s("Bình luận thành công", "success");
                }
                else
                {
                    result = true;
                    Notification.setNotification5s("Đánh giá chỉ được ghi nhận khi bạn đã sản phẩm này", "warning");
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual JsonResult HandleReplyAction(Feedback comment, int productID, string reply_content, int id)
        {
            var user = Session["TaiKhoan"] as Account;
            bool result = false;

            if (user != null)
            {
                int userID = user.acc_id;
                comment.account_id = userID;
                comment.product_id = productID;
                comment.content = reply_content;
                comment.replyfor = id;
                comment.status = "2";
                comment.create_at = DateTime.Now;
                bool hasPurchased = db.Oder_Detail.Any(od => od.Order.acc_id == userID && od.pro_id == productID && od.Order.status == "3");
                if (hasPurchased || user.Role.role_name == "Admin" || user.Role.role_name == "Nhân viên")
                {
                    db.Feedbacks.Add(comment);
                    db.SaveChanges();
                    result = true;
                    Notification.setNotification3s("Phản hồi thành công", "success");
                }
                else
                {
                    result = true;
                    Notification.setNotification5s("Đánh giá chỉ được ghi nhận khi bạn đã sản phẩm này", "warning");
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }

    //kế thừa productcontrollerbase
    public class CustomProductController : ProductControllerBase
    {
        protected override Product GetProductDetail(int id)
        {
            return base.GetProductDetail(id);
        }

        protected override void SetRelatedProduct(Product product)
        {
            base.SetRelatedProduct(product);
        }

        protected override void SetProductImage(int id)
        {
            base.SetProductImage(id);
        }

        protected override void SetFeedbacks()
        {
            base.SetFeedbacks();
        }

        protected override void SetOrderFeedbacks()
        {
            base.SetOrderFeedbacks();
        }

        protected override void SetComment(int currentPage, int pageSize, Product product)
        {
            base.SetComment(currentPage, pageSize, product);
        }

        //kế thừa productcontrollerbase
        public class CustomAction : ProductControllerBase
        {
            [HttpPost]
            [ValidateInput(false)]
            public override JsonResult HandleCommentAction(Feedback comment, int productID, int rateStar, string commentContent)
            {
                return base.HandleCommentAction(comment, productID, rateStar, commentContent);
            }

            [HttpPost]
            [ValidateInput(false)]
            public override JsonResult HandleReplyAction(Feedback comment, int productID, string reply_content, int id)
            {
                return base.HandleReplyAction(comment, productID, reply_content, id);
            }
        }

    }


}
