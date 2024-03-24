//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnPhanMem.Models
{
    using DoAnPhanMem.Visitor;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.Oder_Detail = new HashSet<Oder_Detail>();
            this.ProductImgs = new HashSet<ProductImg>();
        }
    
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public int cate_id { get; set; }
        public int brand_id { get; set; }
        public int discount_id { get; set; }
        public double price { get; set; }
        public int buyturn { get; set; }
        public int quantity { get; set; }
        public string status_ { get; set; }
        public string specification { get; set; }
        public string pro_img { get; set; }
        public string pro_description { get; set; }
        public Nullable<System.DateTime> update_at { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Discount Discount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oder_Detail> Oder_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImg> ProductImgs { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }


        //[ValidateFile(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        [NotMapped]
        public HttpPostedFileBase[] ImageUploadMulti { get; set; }
        public void Accept(IVisitor visitor, int quantity)
        {
            visitor.Visit(this, quantity);
        }

    }
}