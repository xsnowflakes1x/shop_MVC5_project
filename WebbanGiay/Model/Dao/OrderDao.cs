using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class OrderDao
    {
        WebbanGiayDbContext db = null;
        public OrderDao()
        {
            db = new WebbanGiayDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public IEnumerable<Order> ListOrder(int page, int pageSize)
        {
            return db.Orders.Where(x => x.TrangThai == true).OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }
        public bool Update(Order entity)
        {
            try
            {
                var order = db.Orders.Find(entity.ID);
                order.NguoiNhan = entity.NguoiNhan;
                order.SDT = entity.SDT;
                order.DiaChi = entity.DiaChi;
                order.Email = entity.Email;
                order.ThanhTien = entity.ThanhTien;
                order.TrangThai = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update1(Order entity)
        {
            try
            {
                var order = db.Orders.Find(entity.ID);
                order.NguoiNhan = entity.NguoiNhan;
                order.SDT = entity.SDT;
                order.DiaChi = entity.DiaChi;
                order.Email = entity.Email;                            
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Order ViewDetail(long id)
        {
            return db.Orders.Find(id);
        }
        public bool Delete(long id)
        {
            try
            {
                var order = db.Orders.Find(id);
                order.TrangThai = false;
                db.SaveChanges();          
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}