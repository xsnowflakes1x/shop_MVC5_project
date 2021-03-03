using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebbanGiay.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(int page =1, int pageSize =5)
        {
            var dao = new OrderDao();
            var model = dao.ListOrder(page, pageSize);
            return View(model);
        }
        
        public ActionResult Edit(long id)
        {
            var order = new OrderDao().ViewDetail(id);          
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                var result = dao.Update1(order);
                if (result)
                {
                    SetAlert("Cập Nhật Hóa Đơn Thành Công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật sản phẩm thất bại");
                }
                return View("Index");
            }          
            SetAlert("Bạn phải nhập đủ thông tin", "warning");
            return View(order);
        }
        public ActionResult Delete(long id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}