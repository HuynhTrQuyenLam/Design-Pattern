using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Design_Pattern.Abstract
{
    public interface IOrderFactory
    {
        Order CreateOrder();
        OrderDetail CreateOrderDetail();
    }

}
