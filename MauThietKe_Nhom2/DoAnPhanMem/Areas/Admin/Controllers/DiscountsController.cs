using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAnPhanMem.Common.Helpers;
using DoAnPhanMem.Models;
using PagedList;
using System.Globalization;
using DoAnPhanMem.Factory;

namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class DiscountsController : Controller
    {
        private readonly WebshopEntities _db = new WebshopEntities();

        private readonly IDiscountService _discountService;

        public DiscountsController()
        {
            _discountService = DiscountServiceFactory.CreateDiscountService();
        }

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // GET: Areas/Brands
        public ActionResult Index(string search, int? size, int? page)
        {
            var pageSize = (size ?? 15);
            var pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = from a in _db.Discounts
                       orderby a.discount_id ascending
                       select a;
            if (!string.IsNullOrEmpty(search))
            {
                list = from a in _db.Discounts
                       where a.discount_name.Contains(search) || a.discount_price.ToString().Contains(search)
                       orderby a.discount_id ascending
                       select a;
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Create(DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity)
        {
            string result = "false";

            try
            {
                var discount = _discountService.Create(discountStart, discountEnd, discountPrice, discountCode, quantity);
                result = "success";
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(int id, DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity)
        {
            string result = "error";

            try
            {
                _discountService.Edit(id, discountStart, discountEnd, discountPrice, discountCode, quantity);
                result = "success";
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Admin/Discounts/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            string result = "error";

            try
            {
                _discountService.Delete(id);
                result = "success";
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
