namespace DoAnPhanMem.Models
{
    // Lớp Order_Detail
    // Product  
    public partial class Oder_Detail
    {
        public int pro_id { get; set; }
        public int cate_id { get; set; }
        public int discount_id { get; set; }
        public int oder_id { get; set; }
        public double price { get; set; }
        public string status { get; set; }
        public int quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}


