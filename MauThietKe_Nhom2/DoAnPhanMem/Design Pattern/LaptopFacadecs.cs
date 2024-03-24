using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Data.Entity;



namespace DoAnPhanMem.Models
{
    public class LaptopFacade
    {
        // Khai báo lớp LaptopFacade và khởi tạo biến _db kiểu WebshopEntities để tham chiếu đến cơ sở dữ liệu.
        private readonly WebshopEntities _db;
        public LaptopFacade()
        {
            _db = new WebshopEntities();
        } 
        // LẤY THƯƠNG HIỆU  
        // Định nghĩa phương thức GetBrandList nhận vào ba tham số: search(từ khóa tìm kiếm), size(kích thước trang) và page(số trang).
        public IPagedList<Brand> GetBrandList(string search, int? size, int? page)
        {
            // Nó gán giá trị mặc định cho pageSize và pageNumber bằng cách sử dụng toán tử null - coalescing(??)
           //  Nếu giá trị của size hoặc page là null, thì giá trị mặc định là 15 cho pageSize và 1 cho pageNumber. 
            var pageSize = size ?? 15;
            var pageNumber = page ?? 1;
            // List được tạo bằng cách truy vấn các đối tượng trong bảng Brands
            // Danh sách này được sắp xếp theo thuộc tính brand_id tăng dần.
            var list = from a in _db.Brands
                       orderby a.brand_id ascending
                       select a;
            // Nếu tham số search không rỗng, danh sách sẽ được lọc theo tên thương hiệu chứa từ khóa search.
            if (!string.IsNullOrEmpty(search))
            {
                list = from a in _db.Brands
                       where a.brand_name.Contains(search)
                       orderby a.brand_id ascending
                       select a;
            }
            // Sau đó, danh sách được phân trang bằng thư viện PagedList và trả về dưới dạng đối tượng IPagedList<Brand>.
            // Phương thức ToPagedList được gọi trên danh sách list và truyền vào số trang và kích thước trang. 
            return list.ToPagedList(pageNumber, pageSize);
        }
        // TẠO THUƠNG HIỆU 
        // Định nghĩa phương thức CreateBrand nhận vào tham số brandName để tạo một thương hiệu mới.
        public JsonResult CreateBrand(string brandName)
        {
            // Biến result được khởi tạo với giá trị "false" ban đầu. Biến này sẽ lưu trữ kết quả của quá trình tạo thương hiệu.
            string result = "false";

            try
            {
                // Kiểm tra xem nhãn hiệu có tồn tại trong cơ sở dữ liệu hay không trước khi thêm mới.
                Brand checkExist = _db.Brands.SingleOrDefault(m => m.brand_name == brandName);

                if (checkExist != null)
                {
                    // Nếu tồn tại, phương thức trả về kết quả "exist".
                    result = "exist";
                    // Một đối tượng JsonResult được tạo ra để trả về kết quả "exist". 
                    return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                // Nếu thương hiệu không tồn tại, một đối tượng Brand mới được tạo và tên thương hiệu được gán bằng brandName. 
                Brand brand = new Brand { brand_name = brandName };
                // Thương hiệu mới được thêm vào bảng Brands bằng cách sử dụng phương thức Add
                // Sau đó cập nhật cơ sở dữ liệu bằng cách gọi phương thức SaveChanges trên đối tượng cơ sở dữ liệu _db.
                _db.Brands.Add(brand);
                _db.SaveChanges();
                // Biến result được gán giá trị "success" để biểu thị rằng quá trình tạo thương hiệu thành công.  
                result = "success";
                // Biến result được gán giá trị "success" để biểu thị rằng quá trình tạo thương hiệu thành công.  
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch
            {
                
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        // SỬA 
       // Phương thức nhận hai tham số: id đại diện cho ID của thương hiệu cần chỉnh sửa và brandName đại diện cho tên thương hiệu mới
        public JsonResult EditBrand(int id, string brandName)
        {
            // Biến result được khởi tạo với giá trị "error" ban đầu
            // Biến này sẽ lưu trữ kết quả của quá trình chỉnh sửa thương hiệu.
            string result = "error";
            // Một đối tượng Brand được lấy ra từ cơ sở dữ liệu _db.Brands bằng cách sử dụng phương thức FirstOrDefault
            // Kiểm tra xem có một thương hiệu có brand_id tương ứng với id được truyền vào hay không.
            Brand brand = _db.Brands.FirstOrDefault(m => m.brand_id == id);
            // CheckExist lưu trữ kết quả của việc kiểm tra xem đã tồn tại một thương hiệu khác có tên brandName trong CSDL 
            var checkExist = _db.Brands.SingleOrDefault(m => m.brand_name == brandName);

            try
            {
               // Nếu thương hiệu có tên brandName đã tồn tại trong CSDL ( kiểm tra bằng biến checkExist)
                if (checkExist != null)
                {
                    // Biến result được gán giá trị "exist"
                    result = "exist";
                    // Một đối tượng JsonResult chứa giá trị result được trả về với quyền truy cập JsonRequestBehavior.AllowGet.
                    return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                // Nếu thương hiệu không tồn tại
                // Biến result được gán giá trị "success" để biểu thị rằng quá trình chỉnh sửa thành công.
                result = "success";
                // Thuộc tính brand_name của đối tượng brand được gán giá trị mới(brandName) để thay đổi tên thương hiệu.
               brand.brand_name = brandName;
                // Đối tượng brand được đánh dấu là đã được sửa đổi bằng cách đặt State của Entry(brand) thành EntityState.Modified.
                _db.Entry(brand).State = EntityState.Modified;
                // Cơ sở dữ liệu được cập nhật bằng cách gọi phương thức SaveChanges trên đối tượng cơ sở dữ liệu _db.
                _db.SaveChanges();
                // Một đối tượng JsonResult chứa giá trị result được trả về với quyền truy cập JsonRequestBehavior.AllowGet.
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch
            {
                // Trong trường hợp xảy ra lỗi trong quá trình chỉnh sửa, một đối tượng JsonResult chứa giá trị result ban đầu("error")
                // được trả về với quyền truy cập JsonRequestBehavior.AllowGet. 
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        // XÓA 
        // Phương thức nhận một tham số id đại diện cho ID của thương hiệu cần xóa.
        public JsonResult DeleteBrand(int id)
        {
            // Biến result được khởi tạo với giá trị "error" ban đầu
            // Biến này sẽ lưu trữ kết quả của quá trình xóa thương hiệu.
            string result = "error";
            // Biến check kiểm tra xem có sản phẩm nào trong cơ sở dữ liệu có brand_id tương ứng với id được truyền vào
            // bằng cách sử dụng phương thức Any trên tập hợp Products trong cơ sở dữ liệu _db.
            bool check = _db.Products.Any(m => m.brand_id == id);
           // Nếu check trả về giá trị true,  
            if (check)
            {
                // biến result được gán giá trị "exist" và một đối tượng JsonResult chứa giá trị result
                result = "exist";
               // được trả về với quyền truy cập JsonRequestBehavior.AllowGet
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            // Một đối tượng Brand được lấy ra từ csdl _db.Brands bằng cách sử dụng phương thức FirstOrDefault
            // và kiểm tra xem có một thương hiệu có brand_id tương ứng với id được truyền vào hay không.
            Brand brand = _db.Brands.FirstOrDefault(m => m.brand_id == id);

            try
            {
                // Biến result được gán giá trị "delete" để biểu thị rằng quá trình xóa thành công.
                result = "delete";
                // Đối tượng brand được xóa khỏi cơ sở dữ liệu bằng cách sử dụng
                // phương thức Remove trên tập hợp Brands trong cơ sở dữ liệu _db.
                _db.Brands.Remove(brand);
                _db.SaveChanges();
                // Một đối tượng JsonResult chứa giá trị result được trả về với quyền truy cập JsonRequestBehavior.AllowGet.
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch
            {
                // Trong trường hợp xảy ra lỗi trong quá trình xóa, một đối tượng JsonResult chứa
                // giá trị result ban đầu ("error") được trả về với quyền truy cập JsonRequestBehavior.AllowGet.
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        // Một phương thức công khai (public) được gọi để giải phóng tài nguyên đang được sử dụng bởi đối tượng hiện tại.
        public void Dispose()
        {
            // gọi phương thức Dispose với tham số disposing được đặt là true
            // Điều này cho phép phương thức Dispose thực hiện các công việc cần thiết để giải phóng tài nguyên quan trọng.
            Dispose(true);
            // Ngăn chặn việc gọi phương thức hủy (finalizer) của đối tượng sau khi đã được giải phóng tài nguyên
            // Điều này được thực hiện vì chúng ta đã giải phóng tài nguyên bằng phương thức Dispose
            // nên không cần thiết phải gọi hủy nữa.
            GC.SuppressFinalize(this);
        }
        // Một phương thức bảo vệ (protected) và ảo (virtual) để thực hiện việc giải phóng tài nguyên
        // Nó nhận một tham số disposing, cho phép kiểm soát xem liệu phương thức Dispose được gọi từ phương thức Dispose hay từ hủy.
        protected virtual void Dispose(bool disposing)
        {
            // kiểm tra nếu disposing là true, tức là phương thức Dispose được gọi từ phương thức Dispose
            // Nếu điều kiện này đúng, các tài nguyên quan trọng có thể được giải phóng.
            if (disposing)
            {
                // Kiểm tra xem _db (biến đại diện cho một tài nguyên) có khác null hay không
                // tức là tài nguyên cần được giải phóng đã được khởi tạo và sử dụng.
                if (_db != null)
                {
                    // gọi phương thức Dispose trên đối tượng _db để giải phóng tài nguyên mà nó đang sử dụng
                    _db.Dispose();
                }
            }
        }
    }
}



