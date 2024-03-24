using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK.Strategy
{
    public interface IChoice
    {
        (bool success, double discountPrice) TotalAmount(WebshopEntities db, HttpSessionStateBase session, Func<Tuple<List<Product>, List<int>>> GetCart, string code);
    }
}