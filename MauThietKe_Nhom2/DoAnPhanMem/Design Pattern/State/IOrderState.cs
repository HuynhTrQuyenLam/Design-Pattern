using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK.State
{
    public interface IOrderState
    {
        void Process(int orderId, WebshopEntities db);
        void Cancel(int orderId, WebshopEntities db);
    }
}


