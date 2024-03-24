using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{
    //Director: Director chịu trách nhiệm thực hiện các bước build theo một trình tự xác định cụ thể
    //Class Director là tùy chọn để có Client có thể kiểm soát trực tiếp quá trình.
        public class Director
        {
            private IOrderDetailBuilder builder;

            public Director(IOrderDetailBuilder builder)
            {
                this.builder = builder;
            }

            public Oder_Detail ConstructOrderDetail(int productId, int categoryId, int discountId, int orderId, double price, string status, int quantity)
            {
                return builder
                    .SetProductId(productId)
                    .SetCategoryId(categoryId)
                    .SetDiscountId(discountId)
                    .SetOrderId(orderId)
                    .SetPrice(price)
                    .SetStatus(status)
                    .SetQuantity(quantity)
                    .Build();
            }

        }
    }
