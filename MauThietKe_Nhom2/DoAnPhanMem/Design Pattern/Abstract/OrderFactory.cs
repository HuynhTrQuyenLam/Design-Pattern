using DoAnPhanMem.Models;
using DoAnPhanMem.MTK.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DoAnPhanMem.Design_Pattern.Abstract
{
    public class OrderFactory : IOrderFactory
    {
        private readonly WebshopEntities db;

        public OrderFactory(WebshopEntities context)
        {
            db = context;
        }

        public Order CreateOrder()
        {
            return new Order();
        }

        public OrderDetail CreateOrderDetail()
        {
            return new OrderDetail();
        }
    }
    public class OrderDetail
    {
    }

}