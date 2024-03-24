using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnPhanMem.Design_Pattern.Flyweight
{
    public class FlyweightFactory
    {
        private readonly WebshopEntities db;
        private readonly Dictionary<int, IFlyweight> flyweights;

        public FlyweightFactory(WebshopEntities dbContext)
        {
            db = dbContext;
            flyweights = new Dictionary<int, IFlyweight>();
        }

        public IFlyweight GetFlyweight(int key)
        {
            if (flyweights.ContainsKey(key))
            {
                return flyweights[key];
            }
            else
            {
                // Tạo mới flyweight từ cơ sở dữ liệu hoặc các nguồn khác
                Category category = db.Categories.FirstOrDefault(c => c.cate_id == key);
                if (category != null)
                {
                    IFlyweight flyweight = new ConcreteFlyweight
                    {
                        cate_id = category.cate_id,
                        cate_name = category.cate_name
                    };
                    flyweights.Add(key, flyweight);
                    return flyweight;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
