using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Design_Pattern.Flyweight
{
    public class ConcreteFlyweight : IFlyweight
    {
        public int cate_id { get; set; }
        public string cate_name { get; set; }
        public void Operation()
        {
        }
    }

}
