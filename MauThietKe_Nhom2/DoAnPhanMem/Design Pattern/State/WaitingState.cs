using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK.State
{
    public class WaitingState : IOrderState
    {
        public void Process(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null)
            {
                order.status = "1"; // Chuyển sang trạng thái "Đang xác nhận"
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Cancel(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null)
            {
                order.status = "0"; // Hủy đơn hàng
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Complete(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null && order.status == "1") // Chỉ chuyển sang "Hoàn thành" nếu đang ở trạng thái "Đang xử lý"
            {
                order.status = "3"; // Chuyển sang trạng thái "Hoàn thành"
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}

