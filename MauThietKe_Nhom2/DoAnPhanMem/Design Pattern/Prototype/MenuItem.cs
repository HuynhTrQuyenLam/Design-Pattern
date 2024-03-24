using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnPhanMem.Prototype
{
    //  Lớp MenuItem là một lớp con của lớp BasicMenu.
    //  Nó kế thừa tất cả các thuộc tính và phương thức từ lớp cha và triển khai phương thức trừu tượng Clone.
        public class MenuItem : BasicMenu
        {
            // Constructor MenuItem nhận các tham số và gọi constructor của lớp cha BasicMenu bằng từ khóa base.
            // Điều này đảm bảo rằng các thuộc tính của lớp cha được khởi tạo đúng cách. 
        public MenuItem(string url, string iconPath, string title, string activeClass, string textColor)
                : base(url, iconPath, title, activeClass, textColor)
            {
            }
            //  Phương thức Cloneđược triển khai để tạo một bản sao của đối tượng MenuItem.
            // Trong phương thức này, MemberwiseClone được gọi để sao chép các thuộc tính của đối tượng hiện tại. 
            //  Kết quả của phương thức Clone là một đối tượng MenuItem mới, có các thuộc tính giống hệt với đối tượng gốc. 
            // Chú ý rằng MemberwiseClone chỉ sao chép các thuộc tính, không sao chép các tham chiếu đến các đối tượng khác.
            public override BasicMenu Clone()
            {
                return (MenuItem)this.MemberwiseClone();
            }
        }
    }

