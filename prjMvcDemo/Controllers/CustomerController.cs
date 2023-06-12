using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];

            List<CCustomer> datas = null;
            if (string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactory()).queryAll();
            else
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);

            //List<CCustomer> datas = (new CCustomerFactory()).queryAll();
            //return View(datas);
        }

        public ActionResult create()
        {
            return View();
        }
        public ActionResult save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }

        public ActionResult delete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }

        public ActionResult edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List"); 
            CCustomer x = (new CCustomerFactory()).queryByFid((int)id);
            return View(x);
        }

        [HttpPost]
        public ActionResult edit(CCustomer x)
        {
            (new CCustomerFactory()).update(x);
            return RedirectToAction("List");
        }
    }
}