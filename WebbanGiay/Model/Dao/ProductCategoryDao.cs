using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        WebbanGiayDbContext db = null;
        public ProductCategoryDao()
        {
            db = new WebbanGiayDbContext();
        }
        public List<ProductCategory>ListAll()
        {
            return db.ProductCategories.Where(x => x.TrangThai == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
