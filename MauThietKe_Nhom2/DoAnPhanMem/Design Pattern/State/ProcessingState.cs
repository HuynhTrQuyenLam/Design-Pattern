using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK.State
{
    class ProcessingState : IOrderState
    {
        public void Process(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null)
            {
                if (order.status == "1") // Chỉ chuyển sang "Đang xử lý" nếu ở trạng thái "Đang xác nhận"
                {
                    order.status = "2"; // Chuyển sang trạng thái "Đang xử lý"
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (order.status == "2") // Chỉ chuyển sang "Hoàn thành" nếu ở trạng thái "Đang xử lý"
                {
                    order.status = "3"; // Chuyển sang trạng thái "Hoàn thành"
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void Complete(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null && order.status == "2") // Chỉ chuyển sang "Hoàn thành" nếu đang ở trạng thái "Đang xử lý"
            {
                order.status = "3"; // Chuyển sang trạng thái "Hoàn thành"
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Cancel(int orderId, WebshopEntities db)
        {
            // Không thực hiện hủy ở trạng thái này
        }
    }
}

