using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAnPhanMem.Common.Helpers;
using DoAnPhanMem.Design_Pattern.Flyweight;
using DoAnPhanMem.Models;
using PagedList;

namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly WebshopEntities db = new WebshopEntities();

        private readonly FlyweightFactory flyweightFactory;
        public CategoriesController()
        {
            flyweightFactory = new FlyweightFactory(db);
        }

        // GET: Areas/Categories
        public ActionResult Index(string search, int? size, int? page)
        {
            var pageSize = (size ?? 15);
            var pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = from c in db.Categories
                       orderby c.cate_id ascending
                       select c;
            if (!string.IsNullOrEmpty(search))
            {
                list = from c in db.Categories
                       where c.cate_name.Contains(search)
                       orderby c.cate_id ascending
                       select c;
            }
            foreach (var category in list)
            {
                IFlyweight flyweight = flyweightFactory.GetFlyweight(category.cate_id);
                if (flyweight != null)
                {
                    flyweight.Operation();
                }
            }

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Create(string cateName, Category category)
        {
            string result = "false";
            try
            {
                Category checkExist = db.Categories.SingleOrDefault(c => c.cate_name == cateName);
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                category.cate_name = cateName;
                db.Categories.Add(category);
                db.SaveChanges();
                result = "success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Edit(int id, string cateName)
        {
            string result = "error";
           
            Category category = db.Categories.FirstOrDefault(c => c.cate_id == id);
            var checkExist = db.Categories.SingleOrDefault(c => c.cate_name == cateName);
            try
            {
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                result = "success";
                category.cate_name = cateName;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int id)
        {
            string result = "error";
            bool check = db.Products.Any(m => m.brand_id == id);
            if (check)
            {
                result = "exist";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            Category category = db.Categories.FirstOrDefault(c => c.cate_id == id);
            try
            {
                result = "delete";
                db.Categories.Remove(category);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing) db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
