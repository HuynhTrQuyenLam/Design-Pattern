using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using DoAnPhanMem.DTOs;

namespace DoAnPhanMem.Command
{
    public class ViewCommand : ICommand
    {
        private readonly int _productId;

        public ViewCommand(ProductDTOs product)
        {
            _productId = product.product_id;
        }

        public void Execute()
        {
            // Redirect to the Details action
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var url = urlHelper.Action("Details", "ProductsAdmin", new { id = _productId });
            HttpContext.Current.Response.Redirect(url);
        }
    }
}