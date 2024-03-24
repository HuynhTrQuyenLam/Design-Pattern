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
            //gọi hàm trong TemplateMethod.cs
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
            return HandleCommentAction(comment, productID, rateStar, commentContent);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ReplyComment(Feedback comment, int productID, string reply_content, int id)
        {
            return HandleReplyAction(comment, productID, reply_content, id);
        }
    }
}

