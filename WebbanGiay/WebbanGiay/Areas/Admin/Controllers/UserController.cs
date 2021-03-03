using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using WebbanGiay.Common;
using PagedList;

namespace WebbanGiay.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1,int pageSize = 2)
        {
            var dao = new userDao();
            var model = dao.ListAllPaging(searchString,page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var user = new userDao().ViewDetail(id);
            return View(user);

        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login");

        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {              
                var dao = new userDao();
                if(dao.CheckUserName(user.TenTaiKhoan))
                {
                    SetAlert("Tài khoản đã tồn tại", "warning");
                }
                 else if(dao.CheckEmail(user.Email))
                {
                    SetAlert("Email đã tồn tại", "warning");
                }
                else
                {
                    var EncryptedMd5Pas = Encryptor.MD5Hash(user.MatKhau);
                    user.MatKhau = EncryptedMd5Pas;
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Thêm Người Dùng Thành Công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Người Dùng Thất Bại");
                    }
                }
                return View(user);                          
            }
            SetAlert("Bạn phải nhập đủ thông tin", "warning");
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new userDao();
                if(!string.IsNullOrEmpty(user.MatKhau))
                {
                    var EncryptedMd5Pas = Encryptor.MD5Hash(user.MatKhau);
                    user.MatKhau = EncryptedMd5Pas;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập Nhật Người Dùng Thành Công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật Người Dùng Thất Bại");
                }
                return View("Index");
            }
            SetAlert("Bạn phải nhập đủ thông tin", "warning");
            return View(user);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new userDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new userDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}