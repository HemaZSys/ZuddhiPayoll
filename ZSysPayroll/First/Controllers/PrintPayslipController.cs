using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using Rotativa;

namespace First.Controllers
{
    public class PrintPayslipController : Controller
    {
        // GET: PrintPayslip
        AppEntities ctx;
        public PrintPayslipController()
        {
            ctx = new Models.AppEntities();
        }
        public ActionResult Index()
        {
            using (AppEntities1 db = new AppEntities1())
            {
            //    List<Emp> employees = db.Emps.ToList();

            //    if (employees != null)
            //    {
            //        ViewBag.employees = employees;
            //    }              
            }
            //var employee = ctx.Emps.ToList();
            var emps = ctx.Payslips.ToList();
            return View(emps);
        }
        public ActionResult PrintAllReport()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }
        public ActionResult IndexById(int id)
        {
            var emp = ctx.Payslips.Where(e => e.EmployeeId == id).First();
            return View(emp);
        }
        public ActionResult PrintSalarySlip(int id)
        {
            var report = new ActionAsPdf("IndexById", new { id = id });
            return report;
        }
    }
}