using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Factory
{
    public interface IDiscountService
    {
        Discount Create(DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity);
        Discount Edit(int id, DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity);
        void Delete(int id);
    }

}
