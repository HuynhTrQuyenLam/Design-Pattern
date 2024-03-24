using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DoAnPhanMem;
using DoAnPhanMem.Design_Pattern.Abstract;
using DoAnPhanMem.DTOs;
using DoAnPhanMem.Models;
using DoAnPhanMem.MTK.State;
using PagedList;

namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private readonly WebshopEntities db = new WebshopEntities();

        private readonly IOrderFactory orderFactory;
        public OrdersController()
        {
            db = new WebshopEntities();
            orderFactory = new OrderFactory(db);
        }
        public OrdersController(IOrderFactory factory)
        {
            db = new WebshopEntities();
            orderFactory = factory;
        }

        // GET: Areas/Orders
        public ActionResult Index(string search, int? size, int? page)
        {
            var pageSize = size ?? 15;
            var pageNumber = page ?? 1;
            ViewBag.search = search;
            ViewBag.countTrash = db.Orders.Where(a => a.status == "0").Count(); //  đếm tổng sp có trong thùng rác
            var list = from a in db.Orders
                       where a.status != "0"
                       orderby a.oder_date descending
                       select a;
            if (!string.IsNullOrEmpty(search))
            {
                list = from a in db.Orders
                       where a.order_id.ToString().Contains(search)
                       orderby a.oder_date descending
                       select a;
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Trash(string search, int? size, int? page)
        {
            var pageSize = size ?? 15;
            var pageNumber = page ?? 1;
            ViewBag.search = search;
            var list = from a in db.Orders
                       where a.status == "0"
                       orderby a.oder_date descending
                       select a;
            if (!string.IsNullOrEmpty(search))
            {
                list = from a in db.Orders
                       where a.order_id.ToString().Contains(search)
                       orderby a.oder_date descending
                       select a;
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == id);
            ViewBag.ListProduct = db.Oder_Detail.Where(m => m.oder_id == order.order_id).ToList();
            ViewBag.OrderHistory = db.Orders.Where(m => m.acc_id == order.acc_id).OrderByDescending(m => m.oder_date).Take(10).ToList();
            Account acc = db.Accounts.FirstOrDefault(m => m.acc_id == order.acc_id);
            ViewBag.Email = acc.email;
            ViewBag.shipfee = db.Deliveries.FirstOrDefault(m => m.delivery_id == order.delivery_id).price;


            if (order == null)
            {
                Notification.setNotification1_5s("Không tồn tại! (ID = " + id + ")", "warning");
                return RedirectToAction("Index");
            }
            return View(order);
        }


        public JsonResult UpdateOrder(int id, string status)
        {
            string result = "error";
            try
            {
                Order order = db.Orders.FirstOrDefault(m => m.order_id == id);
                if (order != null)
                {
                    switch (status)
                    {
                        case "1": // Đang xác nhận
                            if (order.status != "3") // Nếu không phải trạng thái "Hoàn thành"
                            {
                                order.CurrentState = new WaitingState();
                                result = "success";
                            }
                            else
                            {
                                result = "false"; // Không cho phép chuyển từ "Hoàn thành" về "Đang xác nhận"
                            }
                            break;
                        case "2": // Đang xử lý
                            if (order.status == "1") // Chỉ cho phép chuyển sang "Đang xử lý" nếu trạng thái hiện tại là "Đang xác nhận"
                            {
                                order.CurrentState = new ProcessingState();
                                result = "success";
                            }
                            else
                            {
                                result = "false"; // Không cho phép chuyển từ "Hoàn thành" về "Đang xử lý" hoặc chuyển ngược lại
                            }
                            break;
                        case "3": // Hoàn thành
                            if (order.status == "1") // Chỉ cho phép chuyển từ "Đang xác nhận" sang "Hoàn thành"
                            {
                                WaitingState waitingConfirmationState = new WaitingState();
                                waitingConfirmationState.Complete(id, db);
                                result = "success";
                            }
                            else if (order.status == "2") // Chỉ cho phép chuyển từ "Đang xử lý" sang "Hoàn thành"
                            {
                                ProcessingState processingState = new ProcessingState();
                                processingState.Complete(id, db);
                                result = "success";
                            }
                            else
                            {
                                result = "false"; // Không cho phép chuyển từ "Hoàn thành" về "Đang xử lý" hoặc chuyển ngược lại
                            }
                            break;
                        case "0": // Hủy
                            order.CurrentState.Cancel(id, db);
                            result = "success";
                            break;
                        default:
                            result = "false"; // Nếu status không hợp lệ
                            break;
                    }
                }
                else
                {
                    result = "false"; // Nếu không tìm thấy đơn hàng
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancleOrder(int id)
        {
            string result = "error";
            Order order = db.Orders.FirstOrDefault(m => m.order_id == id);
            try
            {
                if (order != null)
                {
                    switch (order.status)
                    {
                        case "1": // Đang xác nhận
                        case "2": // Đang xử lý
                            order.CurrentState = new CancelledState(); // Thiết lập trạng thái mới cho đơn hàng
                            order.CurrentState.Cancel(order.order_id, db); // Gọi phương thức Cancel() trên trạng thái mới
                            result = "success";
                            break;
                        default:
                            result = "false"; // Không thể hủy đơn hàng ở các trạng thái khác
                            break;
                    }
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    result = "false";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}