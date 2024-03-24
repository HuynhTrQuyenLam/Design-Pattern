using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Prototype
{
    public abstract class BasicMenu
    {
        public string Url { get; set; }
        public string IconPath { get; set; }
        public string Title { get; set; }
        public string ActiveClass { get; set; }
        public string TextColor { get; set; }
        public string Others { get; set; } = string.Empty;
        // Phương thức khởi tạo, nhận các tham số url, iconPath, title, activeClass, textColor và gán giá trị cho các thuộc tính tương ứng.
        public BasicMenu(string url, string iconPath, string title, string activeClass, string textColor)
        {

            Url = url;
            IconPath = iconPath;
            Title = title;
            ActiveClass = activeClass;
            TextColor = textColor;
        }
        // Phương thức để thêm một menu mới. Nhận các tham số url, iconPath, title, activeClass và cập nhật giá trị của các thuộc tính.
        public void AddMenu(string url, string iconPath, string title, string activeClass)
        {
            IconPath = iconPath;
            Url = url;
            ActiveClass = activeClass;
            Title = title;
        }
        // Phương thức Clone là một phương thức trừu tượng được khai báo, nhưng không được triển khai trong lớp BasicMenu.
        // Nó được sử dụng để tạo một bản sao của đối tượng BasicMenu. Các lớp con của BasicMenu sẽ cần triển khai phương thức này. 
        public abstract BasicMenu Clone();
    }
}
