using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Visitor
{
    public interface IVisitor
    {
        void Visit(Product product, int quantity);
    }
}

