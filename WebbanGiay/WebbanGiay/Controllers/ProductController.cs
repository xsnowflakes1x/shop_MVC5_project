using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebbanGiay.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCategory(long id)
        {
            var productcategory = new ProductCategoryDao().ViewDetail(id);
            return View(productcategory);
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.ProductCategory = new ProductCategoryDao().ViewDetail(product.MaLoaiSP.Value);
            ViewBag.ProductCatregory1 = new ProductDao().ListProduct2(id);
            return View(product);
        }
        public ActionResult Category(long id)
        {
            var category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            var model = new ProductDao().ListProduct3(id);
            return View(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
                
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword)
        {
            var model = new ProductDao().Search(keyword);
            ViewBag.keyword = keyword;
            return View(model);
        }
    }
}