using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{
    //Director: Director chịu trách nhiệm thực hiện các bước build theo một trình tự xác định cụ thể
    //Class Director là tùy chọn để có Client có thể kiểm soát trực tiếp quá trình.
    public class OrderDirector
    {
        private IOrderBuilder builder;

        public OrderDirector(IOrderBuilder builder)
        {
            this.builder = builder;
        }

        public Order ConstructOrder(int order_id, int payment_id, int delivery_id, double total, int  acc_id ,
            string status, string order_note, string oder_address, string note, string oderUsername, int oderPhone)
        {
            return builder
                .SetOrderId(order_id)
                .SetPaymentId(payment_id)
                .SetDeliveryId(delivery_id)
                .SetOrderDate(System.DateTime.Now)
                .SetTotal(total)
                .SetAccountId(acc_id)
                .SetStatus(status)
                .SetOrderNote(order_note)
                .SetOrderAddress(oder_address)
                .SetNote(note)
                .SetOrderUsername(oderUsername)
                .SetOrderPhone(oderPhone)
                .AddOrderDetail(new Oder_Detail { /* Set properties of Order_Detail object */ })
                .Build();

    }
    }
}
