
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAnPhanMem.Areas.Admin.Controllers;
using DoAnPhanMem.Common.Helpers;
using DoAnPhanMem.Models;
using PagedList;

namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class BrandsController : Controller
    {
        // Đây là khai báo một biến private chỉ đọc (readonly) có kiểu dữ liệu LaptopFacade  
        // tên biến là _laptopFacade. Biến này được sử dụng để tham chiếu đến một đối tượng LaptopFacade
        private readonly LaptopFacade _laptopFacade;

        public BrandsController()
        {
            // Trong phương thức khởi tạo, dòng này khởi tạo một đối tượng mới của lớp LaptopFacade và gán vào biến _laptopFacade
            // Điều này đảm bảo rằng mỗi khi một đối tượng BrandsController được tạo
            // nó sẽ có một đối tượng LaptopFacade tương ứng để sử dụng trong quá trình xử lý.
            _laptopFacade = new LaptopFacade();
        }

        // GET: Areas/Brands
        public ActionResult Index(string search, int? size, int? page)
        {
            var pageSize = (size ?? 15);
            var pageNumber = (page ?? 1);
            ViewBag.search = search;
            // Biến list được gán giá trị bằng cách gọi phương thức GetBrandList trên đối tượng _laptopFacade
            // Phương thức này nhận các tham số search, size, page và trả về một danh sách các thương hiệu dựa trên các tham số tương ứng
            var list = _laptopFacade.GetBrandList(search, size, page);
            // phương thức trả về một View có tên là "Index" và truyền danh sách thương hiệu list tới View để hiển thị danh sách trên trang web.
            return View(list);
        }

        [HttpPost]
        public JsonResult Create(string brandName, Brand brand)
        {
            // Biến result được khởi tạo với giá trị "false" ban đầu.
            // Biến này sẽ lưu trữ kết quả của quá trình tạo mới thương hiệu.
            string result = "false";
            try
            {
                // Được gọi để tạo mới thương hiệu bằng cách chuyển brandName như là một tham số.
                // Kết quả của phương thức này được gán cho biến createResult. 
                JsonResult createResult = _laptopFacade.CreateBrand(brandName);
                return createResult;
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Edit(int id, string brandName)
        {
            // Đây là khai báo một biến "result" có kiểu dữ liệu là "string" và khởi tạo nó với giá trị "error"
            // Biến này sẽ được sử dụng để lưu trữ kết quả của phương thức Edit.
            string result = "error";
            try
            {
                // Gọi phương thức "EditBrand" của đối tượng "_laptopFacade" với tham số là "id" và "brandName".
                // Phương thức này được gọi để chỉnh sửa thông tin của một thương hiệu laptop.
                // Kết quả của phương thức được gán vào biến "editResult" có kiểu dữ liệu là "JsonResult".
                JsonResult editResult = _laptopFacade.EditBrand(id, brandName);
                // Trả về kết quả của phương thức "EditBrand"
                // Khi phương thức "EditBrand" được gọi, kết quả sẽ được trả về từ phương thức này.
                return editResult;
            }
            catch
            {
                // Trả về một đối tượng JsonResult chứa kết quả "result" (trong trường hợp xảy ra lỗi)
                // và cho phép xác thực GET (JsonRequestBehavior.AllowGet)
                // Điều này cho phép truy cập vào phương thức này thông qua phương thức HTTP GET.
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            // một biến result được khởi tạo với giá trị mặc định là "error" 
            string result = "error";
            try
            {
                // phương thức thử xóa một thương hiệu laptop bằng cách gọi phương thức DeleteBrand từ đối tượng _laptopFacade với tham số id.
                // Kết quả trả về của phương thức DeleteBrand được gán cho biến deleteResult.
                JsonResult deleteResult = _laptopFacade.DeleteBrand(id);
                return deleteResult;
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _laptopFacade.Dispose();
            base.Dispose(disposing);
        }
    }
}
