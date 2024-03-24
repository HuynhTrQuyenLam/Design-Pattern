using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DoAnPhanMem.Factory_Method
{
    // Factory class for creating Product objects
    public class ProductFactory
    {
        public static Product CreateProduct(string name, int quantity, string description, string specification, int discountId, double price, int brandId, int categoryId, HttpPostedFileBase imageUpload)
        {
            Product product = new Product();
            // Perform complex initialization here
            product.pro_name = name;
            product.quantity = quantity;
            product.pro_description = description;
            product.specification = specification;
            product.discount_id = discountId;
            product.price = price;
            product.brand_id = brandId;
            product.cate_id = categoryId;
            product.update_at = DateTime.Now;

            // Handle image upload
            if (imageUpload != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(imageUpload.FileName);
                var extension = Path.GetExtension(imageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("HH-mm-dd-MM-yyyy") + extension;
                product.pro_img = "/Content/Images/product/" + fileName;
                imageUpload.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/product/"), fileName));
            }
            else
            {
                // Handle error if image is not provided
                throw new Exception("Vui lòng thêm Ảnh Thumbnail!");
            }

            return product;
        }
    }

}