using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace First.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Employee
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }
        [HandleError]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Email"] = "";
            Session["Id"] = "";
            Session["AccessType"] = "";
            Session.Abandon();
            return RedirectToAction("Index", "UserLogin");

        }
    }
}