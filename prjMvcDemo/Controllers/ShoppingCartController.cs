using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart

        public ActionResult CartView()
        {
            List<CShoppingCartltem> cart= 
                Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartltem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }
        public ActionResult List()
        {
            dbDemoEntities db = new dbDemoEntities();
            var datas = from c in db.tProduct
                        select c;
            return View(datas);
        }

        public ActionResult AddToSession(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == vm.txtFId);
            if (prod != null)
            {
                List<CShoppingCartltem> cart = Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartltem>;
                if (cart == null)
                {
                    cart = new List<CShoppingCartltem>();
                    Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] = cart;
                }
                CShoppingCartltem item = new CShoppingCartltem();
                item.price = (decimal)prod.fPrice;
                item.productId = vm.txtFId;
                item.count = vm.txtCount;
                cart.Add(item);
            }
            return RedirectToAction("List");
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == vm.txtFId);
            if (prod != null)
            {
                tShoppingCart x = new tShoppingCart();
                x.fProductId = vm.txtFId;
                x.fPrice = prod.fPrice;
                x.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                x.fCount = vm.txtCount;
                x.fCustomerId = 1;
                db.tShoppingCart.Add(x);
                db.SaveChanges();
            }
                return RedirectToAction("List");
        }

    }
}