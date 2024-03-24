using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{
    //Concrete Builder: tuân theo Builder Interface cung cấp các triển khai cụ thể của từng bước 
    public class OrderDetailConcreteBuilder : IOrderDetailBuilder
        {
            private Oder_Detail orderDetail;

            public OrderDetailConcreteBuilder()
            {
                orderDetail = new Oder_Detail();
            }

            public IOrderDetailBuilder SetProductId(int productId)
            {
                orderDetail.pro_id = productId;
                return this;
            }

            public IOrderDetailBuilder SetCategoryId(int categoryId)
            {
                orderDetail.cate_id = categoryId;
                return this;
            }

            public IOrderDetailBuilder SetDiscountId(int discountId)
            {
                orderDetail.discount_id = discountId;
                return this;
            }

            public IOrderDetailBuilder SetOrderId(int oderId)
            {
                orderDetail.oder_id = oderId;
                return this;
            }

            public IOrderDetailBuilder SetPrice(double price)
            {
                orderDetail.price = price;
                return this;
            }

            public IOrderDetailBuilder SetStatus(string status)
            {
                orderDetail.status = status;
                return this;
            }

            public IOrderDetailBuilder SetQuantity(int quantity)
            {
                orderDetail.quantity = quantity;
                return this;
            }

            public Oder_Detail Build()
            {
                return orderDetail;
            }
           

        }
    }
