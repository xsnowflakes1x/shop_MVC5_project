using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class userDao
    {
        WebbanGiayDbContext db = null;
        public userDao()
        {
            db = new WebbanGiayDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.MaUser;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.MaUser);
                user.HoTen = entity.HoTen;
                if(!string.IsNullOrEmpty(entity.MatKhau))
                {
                    user.MatKhau = entity.MatKhau;
                }
                user.DiaChi = entity.DiaChi;
                user.Email = entity.Email;
                user.CMND = entity.CMND;
                user.SDT = entity.SDT;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public IEnumerable<User> ListAllPaging(string searchString,int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTaiKhoan.Contains(searchString) || x.HoTen.Contains(searchString));
            }
            return model.OrderByDescending(x=>x.NgaySinh).ToPagedList(page, pageSize);
        }
        public User GetById(string UserName)
        {
            return db.Users.SingleOrDefault(x => x.TenTaiKhoan == UserName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public int Login(string userName,string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.TenTaiKhoan == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.LaAdmin == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MatKhau == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public int LoginUser(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.TenTaiKhoan == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MatKhau == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.TenTaiKhoan == userName) > 0;
        }
        public bool CheckEmail(string Email)
        {
            return db.Users.Count(x => x.Email == Email) > 0;
        }
    }
}
