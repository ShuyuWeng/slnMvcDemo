using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            dbDemoEntities db = new dbDemoEntities();
            IEnumerable<tProduct> datas = null;
            if (string.IsNullOrEmpty(keyword))
                datas = from c in db.tProduct
                        select c;
            else
                datas = db.tProduct.Where(p=>p.fName.Contains(keyword) || p.fQty.Value.ToString().Contains(keyword) );
            return View(datas);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create( tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (prod != null)
            {
                db.tProduct.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
           dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]

        public ActionResult Edit(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == p.fId);
            if (prod != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    p.photo.SaveAs(Server.MapPath("../../Images/" + photoName));
                    prod.fImagePath = photoName;
                }
                prod.fName = p.fName;
                prod.fQty = p.fQty;
                prod.fCost = p.fCost;
                prod.fPrice = p.fPrice;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}