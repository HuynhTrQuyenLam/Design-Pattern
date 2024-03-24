using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Builder
{

    // Builder cho lớp Order_Detail
    //Builder Interface: chỉ định các phương phương pháp để tạo các phần khác nhau Product object 
    public interface IOrderDetailBuilder
        {
            IOrderDetailBuilder SetProductId(int productId);
            IOrderDetailBuilder SetCategoryId(int categoryId);
            IOrderDetailBuilder SetDiscountId(int discountId);
            IOrderDetailBuilder SetOrderId(int oderId);
            IOrderDetailBuilder SetPrice(double price);
            IOrderDetailBuilder SetStatus(string status);
            IOrderDetailBuilder SetQuantity(int quantity);
            Oder_Detail Build();
        }

    
}

