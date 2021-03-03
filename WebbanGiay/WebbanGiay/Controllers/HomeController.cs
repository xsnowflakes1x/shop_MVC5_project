using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebbanGiay.Common;
using WebbanGiay.Models;

namespace WebbanGiay.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home   
        public ActionResult Index(int page=1,int pageSize=12)
        { 
            
            var model = new ProductDao().ListProduct(page,pageSize);         
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if(cart !=null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        public ActionResult SapXep(int page =1, int pageSize=12)
        {         
            var model1 = new ProductDao().ListProduct4(page,pageSize);               
            return View(model1);
        }
    }
}