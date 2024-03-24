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
    }
}
