using BotDetect.Web.UI.Mvc;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebbanGiay.Common;
using WebbanGiay.Models;

namespace WebbanGiay.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Home/Index");
           
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new userDao();
                var result = dao.LoginUser(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new userLogin();
                    userSession.UserName = user.TenTaiKhoan;
                    userSession.UserID = user.MaUser;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/Home/Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }
            return View(model);
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new userDao();
                if(dao.CheckUserName(model.TenTaiKhoan))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if(dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.TenTaiKhoan = model.TenTaiKhoan;
                    user.MatKhau = Encryptor.MD5Hash(model.MatKhau);
                    user.HoTen = model.HoTen;
                    user.DiaChi = model.DiaChi;
                    user.SDT = model.SDT;
                    user.CMND = model.CMND;
                    user.Email = model.Email;
                    user.TrangThai = true;         
                    var result = dao.Insert(user);
                    if(result >0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng Ký Không Thành công");
                    }
                }
              
            }
            return View(model);
        }
    }
}