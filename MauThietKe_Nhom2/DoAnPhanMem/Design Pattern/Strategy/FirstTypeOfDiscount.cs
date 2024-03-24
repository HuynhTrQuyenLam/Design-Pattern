using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnPhanMem.Models; // giả sử đây là namespace của model của bạn
using DoAnPhanMem.MTK.Strategy;
using static DoAnPhanMem.Controllers.CartController;

namespace DoAnPhanMem.Controllers
{
    public class FirstTypeOfDiscount : IChoice
    {
        public (bool success, double discountPrice) TotalAmount(WebshopEntities db, HttpSessionStateBase session, Func<Tuple<List<Product>, List<int>>> GetCart, string code)
        {
            var cart = GetCart(); // Giả sử GetCart() là một phương thức để lấy các mục trong giỏ hàng
            var products = cart.Item1;
            double productPrice = 0d;
            var discount = db.Discounts.SingleOrDefault(d => d.discount_code == code);
            for (int i = 0; i < products.Count; i++)
            {
                var item = products[i];
                productPrice = item.price;
            }

            session["Discountcode"] = discount.discount_code;
            double discountPrice = discount.discount_price;
            session["Discount"] = discountPrice;
            return (true, discountPrice);

        }
    }
}
