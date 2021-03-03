using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using PagedList;

namespace WebbanGiay.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public ActionResult Edit(long id)
        {
            var sanpham = new ProductDao().ViewDetail(id);
            SetViewBag();
            return View(sanpham);
        }

        [HttpPost]
        public ActionResult Create(Product sanpham)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (dao.CheckNameProduct(sanpham.TenSanPham))
                {
                    SetAlert("Tên sản phẩm đã tồn tại", "warning");
                    SetViewBag();
                    return View();
                }
                else
                {
                    long id = dao.Insert(sanpham);
                    if (id > 0)
                    {
                        SetAlert("Thêm Sản Phẩm Thành Công", "success");
                        SetViewBag();
                        return RedirectToAction("Index", "Product");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm sản phẩm thất bại");
                    }
                }
            }
            SetAlert("Ban Phải nhập đủ thông tin", "warning");
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Product sanpham)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(sanpham);
                if (result)
                {
                    SetAlert("Cập Nhật Sản Phẩm Thành Công", "success");
                    return RedirectToAction("Index", "Product");

                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật sản phẩm thất bại");
                }
            }
            SetAlert("Ban Phải nhập đủ thông tin", "warning");
            SetViewBag();
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.MaLoaiSP = new SelectList(dao.ListAll(), "MaLoaiSP", "TenLoaiSP", selectedID);
        }
    }
}