using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebbanGiay.Models;
using System.Web.Script.Serialization;
using Model.EF;
using WebbanGiay.Common;

namespace WebbanGiay.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        private const string USER_SESSION = "USER_SESSION";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productID, int soluong)
        {
            var product = new ProductDao().ViewDetail(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {

                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productID))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.SoLuong += soluong;
                        }
                    }
                }
                else
                {
                    // Tạo mới đối tượng giỏ hàng
                    var item = new CartItem();
                    item.Product = product;
                    item.SoLuong = soluong;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                // Tạo mới đối tượng giỏ hàng
                var item = new CartItem();
                item.Product = product;
                item.SoLuong = soluong;
                var list = new List<CartItem>();
                list.Add(item);
                // gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipname, string mobile, string address, string email)
        {
            var session = (userLogin)Session[CommonConstants.USER_SESSION];          
            var order = new Order();
            if (session == null)
            {
                order.NgayTao = DateTime.Now;
                order.DiaChi = address;
                order.SDT = mobile;
                order.Email = email;
                order.NguoiNhan = shipname;
            }
            else
            {
                order.MaUser = session.UserID; 
                order.NgayTao = DateTime.Now;
                order.DiaChi = address;
                order.SDT = mobile;
                order.Email = email;
                order.NguoiNhan = shipname;
            }
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal TongThanhToan = 0;         
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.MaSP = item.Product.ID;
                    orderDetail.MaGioHang = id;
                    orderDetail.DonGia = item.Product.GiaBan;
                    orderDetail.SoLuong = item.SoLuong;
                    detailDao.Insert(orderDetail);
                    TongThanhToan += (item.Product.GiaBan.GetValueOrDefault(0) * item.SoLuong);                
                }             
                order.ThanhTien = TongThanhToan;
                new OrderDao().Update(order);                                                                                                                                     
            }
            catch (Exception ex)
            {              
                return Redirect("/");       
            }                       
            return Redirect("/hoan-thanh");
        }
        public ActionResult Susscess()
        {
            Session[CartSession] = null;
            return View();
        }
    }
}