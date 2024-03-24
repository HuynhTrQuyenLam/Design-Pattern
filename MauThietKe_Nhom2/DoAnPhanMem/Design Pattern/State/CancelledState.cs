using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK.State
{
    // Lớp này có nhiệm vụ xử lý hủy đơn hàng.
    // Phương thức Cancel thực hiện việc cập nhật trạng thái của đơn hàng trong cơ sở dữ liệu sang hủy đơn hàng.
    class CancelledState : IOrderState
    {
        public void Process(int orderId, WebshopEntities db)
        {
            // Không thực hiện xử lý ở trạng thái này
        }

        public void Cancel(int orderId, WebshopEntities db)
        {
            Order order = db.Orders.FirstOrDefault(m => m.order_id == orderId);
            if (order != null)
            {
                order.status = "0"; // Chuyển sang trạng thái "Cancelled"
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}

