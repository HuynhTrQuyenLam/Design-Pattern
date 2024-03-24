using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Linq.Expressions;

namespace DoAnPhanMem.Controllers
{
    public class ProductController : ProductControllerBase
    {
        // GET: Product
        WebshopEntities _db = new WebshopEntities();
        public ActionResult Index(int? page)
        {
            return View(GetProduct(m => m.status_ == "1", page));
        }
        public ActionResult ProductsByCategory(int categoryId, int? page)
        {
            ViewBag.Type = db.Categories.FirstOrDefault(m => m.cate_id == categoryId)?.cate_name;
            return View("Index", GetProduct(m => m.status_ == "1" && m.cate_id == categoryId, page));
        }
        public ActionResult ProductsByBrand(int brandId, int? page)
        {
            ViewBag.Type = db.Brands.FirstOrDefault(m => m.brand_id == brandId).brand_name;
            return View("Index", GetProduct(m => m.status_ == "1" && m.brand_id == brandId, page));
        }
        public ActionResult SearchResult(int? page, string s)
        {
            ViewBag.Type = "Kết quả tìm kiếm - " + s;
            return View("Index", GetProduct(m => m.status_ == "1" && (m.pro_name.Contains(s) || m.pro_id.ToString().Contains(s)), page));
        }
        public ActionResult ProductDetail(int id, int? page)
        {
            int pageSize = 1;
            int currentPage = page ?? 1;
            var product = GetProductDetail(id);
            if (product == null)
            {
                return Redirect("/");
            }
            SetRelatedProduct(product);
            SetProductImage(id);
            SetFeedbacks();
            SetOrderFeedbacks();
            SetComment(currentPage, pageSize, product);
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ProductComment(Feedback comment, int productID, int rateStar, string commentContent)
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
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
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
}
