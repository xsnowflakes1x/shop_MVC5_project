using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class ProductDao
    {
        WebbanGiayDbContext db = null;

        public ProductDao()
        {
            db = new WebbanGiayDbContext();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Product entity)
        {
            try
            {

                var sanpham = db.Products.Find(entity.ID);
                sanpham.TenSanPham = entity.TenSanPham;
                sanpham.MoTa = entity.MoTa;
                sanpham.GiaBan = entity.GiaBan;
                sanpham.SoLuong = entity.SoLuong;
                sanpham.HinhAnh = entity.HinhAnh;
                sanpham.ChiTiet = entity.ChiTiet;
                sanpham.MaLoaiSP = entity.MaLoaiSP;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString));
            }
            return model.Where(x=>x.TrangThai==true).OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public bool Delete(long id)
        {
            try
            {
                var sanpham = db.Products.Find(id);
                sanpham.TrangThai = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.TrangThai = !product.TrangThai;
            db.SaveChanges();
            return product.TrangThai;
        }
        public IEnumerable<Product> ListProduct( int page, int pageSize)
        {
            var model = db.Products.Where(x => x.TrangThai == true).OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
            return model;
        }
        /// <summary>
        /// lay danh sach lien quan 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProduct2(long id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(x => x.ID != id && x.MaLoaiSP == product.MaLoaiSP && x.TrangThai==true).ToList();
        }
        /// <summary>
        /// lay danh sach san pham theo danh muc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProduct3(long id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(x => x.MaLoaiSP == id && x.TrangThai==true).ToList();
        }
        public bool CheckNameProduct(string name)
        {
            return db.Products.Count(x => x.TenSanPham == name) > 0;
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.TenSanPham.Contains(keyword)).Select(x => x.TenSanPham).ToList();
        }
        public List<Product> Search(string keyword)
        {          
            return db.Products.Where(x => x.TenSanPham == keyword).ToList();
        }
        //lay danh sach San Pham Gia Giam dan
        public IEnumerable<Product> ListProduct4(int page, int pageSize)
        {        
            return db.Products.Where(x => x.TrangThai == true).OrderByDescending(x => x.GiaBan).ToPagedList(page,pageSize);
        }

    }
}
