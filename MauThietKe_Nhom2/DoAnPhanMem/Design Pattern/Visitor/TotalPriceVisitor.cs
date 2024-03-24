using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Visitor
{
    public class TotalPriceVisitor : IVisitor
    {
        public double TotalPrice { get; private set; }
        // Concrete Visitor tính tổng giá tiền của các sản phẩm
        // Lớp này được sử dụng để tính tổng giá tiền của các sản phẩm bằng cách duyệt qua từng sản phẩm và số lượng tương ứng.
        public void Visit(Product product, int quantity)
        {
            double productPrice = product.price;
            if (product.Discount != null && product.Discount.discount_start < DateTime.Now && product.Discount.discount_end > DateTime.Now)
            {
                productPrice -= product.Discount.discount_price;
            }
            TotalPrice += productPrice * quantity;
        }
    }
}
