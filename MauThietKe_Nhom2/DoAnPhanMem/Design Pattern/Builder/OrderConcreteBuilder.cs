using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{
    //Concrete Builder: tuân theo Builder Interface cung cấp các triển khai cụ thể của từng bước
    public class OrderConcreteBuilder : IOrderBuilder
        {
            private Order order;

            public OrderConcreteBuilder()
            {
                order = new Order();
                order.Oder_Detail = new List<Oder_Detail>();
            }

            public IOrderBuilder SetOrderId(int orderId)
            {
                order.order_id = orderId;
                return this;
            }

            public IOrderBuilder SetPaymentId(int paymentId)
            {
                order.payment_id = paymentId;
                return this;
            }

            public IOrderBuilder SetDeliveryId(int deliveryId)
            {
                order.delivery_id = deliveryId;
                return this;
            }

            public IOrderBuilder SetOrderDate(DateTime oder_date)
            {
                order.oder_date = oder_date;
                return this;
            }

            public IOrderBuilder SetTotal(double total)
            {
                order.total = total;
                return this;
            }

            public IOrderBuilder SetAccountId(int accountId)
            {
                order.acc_id = accountId;
                return this;
            }

            public IOrderBuilder SetStatus(string status)
            {
                order.status = status;
                return this;
            }

            public IOrderBuilder SetOrderNote(string orderNote)
            {
                order.order_note = orderNote;
                return this;
            }

            public IOrderBuilder SetOrderAddress(string oder_address)
            {
                order.oder_address = oder_address;
                return this;
            }

            public IOrderBuilder SetNote(string note)
            {
                order.note = note;
                return this;
            }

            public IOrderBuilder SetOrderUsername(string oderUsername)
            {
                order.oderUsername = oderUsername;
                return this;
            }

            public IOrderBuilder SetOrderPhone(int? oderPhone)
            {
                order.oderPhone = oderPhone;
                return this;
            }

            public IOrderBuilder AddOrderDetail(Oder_Detail orderDetail)
            {
                order.Oder_Detail.Add(orderDetail);
                return this;
            }

            public Order Build()
            {
                return order;
            }
        }
    }
