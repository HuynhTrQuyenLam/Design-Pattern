using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Factory
{
    public class DiscountService : IDiscountService
    {
        private readonly WebshopEntities _db;

        public DiscountService(WebshopEntities db)
        {
            _db = db;
        }

        public Discount Create(DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity)
        {
            var discount = new Discount
            {
                discount_name = $"Giảm {discountPrice.ToString("#,0₫")}",
                discount_price = discountPrice,
                quantity = quantity,
                discount_start = discountStart,
                discount_end = discountEnd,
                discount_code = discountCode
            };

            _db.Discounts.Add(discount);
            _db.SaveChanges();

            return discount;
        }

        public Discount Edit(int id, DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity)
        {
            var discount = _db.Discounts.FirstOrDefault(m => m.discount_id == id);

            if (discount != null)
            {
                discount.discount_name = $"Giảm {discountPrice.ToString("#,0₫")}";
                discount.discount_price = discountPrice;
                discount.discount_start = discountStart;
                discount.discount_end = discountEnd;
                discount.quantity = quantity;
                discount.discount_code = discountCode;

                _db.Entry(discount).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return discount;
        }

        public void Delete(int id)
        {
            var discount = _db.Discounts.FirstOrDefault(m => m.discount_id == id);

            if (discount != null)
            {
                _db.Discounts.Remove(discount);
                _db.SaveChanges();
            }
        }
    }

}
