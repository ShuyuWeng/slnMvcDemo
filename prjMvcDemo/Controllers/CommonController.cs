using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session[CDictionary.SK_LOGINED_USER] == null)
                return RedirectToAction("Login");
            return View();
        }

        [HttpPost]

        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomer cust = (new CCustomerFactory()).queryByEmail(vm.txtAccount);
            if (cust != null)
            {
                if (cust.fPassword.Equals(vm.txtPassword))
                {
                    return RedirectToAction("Home");
                }
            }
            return View();
        }
    }
}