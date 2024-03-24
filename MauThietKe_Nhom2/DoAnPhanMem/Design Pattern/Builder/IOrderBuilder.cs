using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{
    //Builder Interface: chỉ định các phương phương pháp để tạo các phần khác nhau Product object 
    public interface IOrderBuilder
        {
            IOrderBuilder SetOrderId(int orderId);
            IOrderBuilder SetPaymentId(int paymentId);
            IOrderBuilder SetDeliveryId(int deliveryId);
            IOrderBuilder SetOrderDate(DateTime orderDate);
            IOrderBuilder SetTotal(double total);
            IOrderBuilder SetAccountId(int accountId);
            IOrderBuilder SetStatus(string status);
            IOrderBuilder SetOrderNote(string orderNote);
            IOrderBuilder SetOrderAddress(string orderAddress);
            IOrderBuilder SetNote(string note);
            IOrderBuilder SetOrderUsername(string orderUsername);
            IOrderBuilder SetOrderPhone(int? orderPhone);
            IOrderBuilder AddOrderDetail(Oder_Detail oderDetail);
            Order Build();
        }
    }
