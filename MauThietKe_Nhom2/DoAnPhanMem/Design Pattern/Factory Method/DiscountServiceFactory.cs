using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Factory
{
    public class DiscountServiceFactory
    {
        public static IDiscountService CreateDiscountService()
        {
            return new DiscountService(new WebshopEntities());
        }
    }

}

