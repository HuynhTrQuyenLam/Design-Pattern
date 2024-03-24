using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.MTK
{
    public class LaptopSingleton
    {
        
        private static WebshopEntities _instance; // Dòng này khai báo một biến tĩnh (static) _instance kiểu WebshopEntities.
                                                  // Biến này sẽ lưu trữ một thể hiện duy nhất của lớp WebshopEntities.

        // 
        private LaptopSingleton() { }

      //  Mã trên triển khai mẫu thiết kế Singleton, đảm bảo rằng chỉ có một thể hiện duy nhất của lớp WebshopEntities
      //  được tạo ra và sử dụng trong ứng dụng.
            public static WebshopEntities GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new WebshopEntities();
                }
                return _instance;
            }
        }

    }

