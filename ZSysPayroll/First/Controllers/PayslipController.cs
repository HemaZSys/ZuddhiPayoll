using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using System.Data.SqlClient;
using System.Net;
using System.Data;
using System.Configuration;

namespace First.Controllers
{
    public class PayslipController : Controller
    {
         //1. *************show the list of Payslip******************
         //GET: Home
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;       
        public ActionResult Index()
        {
            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.ToList();
                List<Payslip> PayslipDetails = db.Payslips.ToList();
               

                var employeeRecord = from e in employees
                                     join d in PayslipDetails on e.Id equals d.EmployeeId into table1
                                     from d in table1.ToList()
                                     //join i in incentives on e.Incentive_Id equals i.IncentiveId into table2
                                     //from i in table2.ToList()
                                     select new ViewModel
                                     {
                                         Employee = e,
                                         Payslip = d
                                         
                                     };
                return View(employeeRecord);
            }
        }

        // GET: Home/PayslipDetails/5
        public ActionResult PayslipDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payslip Payslip = new Payslip();
            string query = "SELECT * FROM Payslip where PId=" + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Payslip = new Payslip
                            {
                                PId = Convert.ToInt32(sdr["PId"]),
                                Basic = Convert.ToString(sdr["Basic"]),
                                HRA = Convert.ToString(sdr["HRA"]),
                                //Special_allowance = Convert.ToString(sdr["Special_allowance"]),
                                Gross_Total = Convert.ToString(sdr["Gross_Total"]),
                               PF = Convert.ToString(sdr["PF"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (Payslip == null)
            {
                return HttpNotFound();
            }
            return View(Payslip);
        }

        // 2. *************ADD NEW Payslip Details ******************
        // GET: Home/CreatePayslip
        public ActionResult CreatePayslip()
        {
            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.ToList();

                if (employees != null)
                {
                    ViewBag.employees = employees;
                }

                return View();
            }
        }

        // POST: Home/CreatePayslip
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePayslip([Bind(Include = "EmployeeId,Name,Month,Basic,HRA,DA,Coveyance,Bonus,Gross_Total,PF,Incometax,Professiontax,SalaryAdv,Others,Deduction_Total,Total_Sal")] PayslipModelVeiw PayslipModelVeiw)
        {
            string employeeid = Request.Form["Name"].ToString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Payslip data into database                
                using (SqlCommand cmd = new SqlCommand("SP_payslipdetail_create", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", PayslipModelVeiw.Month);
                    cmd.Parameters.AddWithValue("@Basic", PayslipModelVeiw.Basic);
                    cmd.Parameters.AddWithValue("@HRA", PayslipModelVeiw.HRA);
                    cmd.Parameters.AddWithValue("@DA", PayslipModelVeiw.DA);
                    cmd.Parameters.AddWithValue("@Coveyance", PayslipModelVeiw.Coveyance);
                    cmd.Parameters.AddWithValue("@Bonus", PayslipModelVeiw.Bonus);
                    cmd.Parameters.AddWithValue("@Gross_Total", PayslipModelVeiw.Gross_Total);
                    cmd.Parameters.AddWithValue("@PF", PayslipModelVeiw.PF);
                    cmd.Parameters.AddWithValue("@Incometax", PayslipModelVeiw.Incometax);
                    cmd.Parameters.AddWithValue("@Professiontax", PayslipModelVeiw.Professiontax);
                    cmd.Parameters.AddWithValue("@SalaryAdv", PayslipModelVeiw.SalaryAdv);
                    cmd.Parameters.AddWithValue("@Others", PayslipModelVeiw.Others);
                    cmd.Parameters.AddWithValue("@Deduction_Total", PayslipModelVeiw.Deduction_Total);
                    cmd.Parameters.AddWithValue("@Total_Sal", PayslipModelVeiw.Total_Sal);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeid);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("Index");

            // return View(PayslipModelVeiw);
        }
        

        // 3. *************Update Payslip Detail ******************

        // GET: Home/UpdatePayslip/5
        public ActionResult UpdatePayslip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayslipModelVeiw Payslip = new PayslipModelVeiw();
            string query = "SELECT p.*,e.[Name] FROM Payslip p join Emp e on p.EmployeeId = e.Id where p.PId = " + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Payslip = new PayslipModelVeiw
                            {                           
                               Month = Convert.ToDateTime(sdr["MonthYear"]),
                                Basic = Convert.ToString(sdr["Basic"]),
                                HRA = Convert.ToString(sdr["HRA"]),
                                DA = Convert.ToString(sdr["DA"]),
                                Coveyance = Convert.ToString(sdr["Coveyance"]),
                                Bonus = Convert.ToString(sdr["Bonus"]),
                                Gross_Total = Convert.ToString(sdr["Gross_Total"]),
                                PF = Convert.ToString(sdr["PF"]),
                                Incometax = Convert.ToString(sdr["Incometax"]),
                                Professiontax = Convert.ToString(sdr["Professiontax"]),
                                SalaryAdv = Convert.ToString(sdr["SalaryAdv"]),
                                Others = Convert.ToString(sdr["Others"]),
                                Deduction_Total = Convert.ToString(sdr["Deduction_Total"]),
                                Total_Sal = Convert.ToString(sdr["Total_sal"]),
                                EmployeeId = Convert.ToInt32(sdr["EmployeeId"]),
                                Name = Convert.ToString(sdr["Name"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (Payslip == null)
            {
                return HttpNotFound();
            }
            return View(Payslip);
        }

        // POST: Home/UpdatePayslip/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePayslip([Bind(Include = "EmployeeId,Name,Month,Basic,HRA,DA,Coveyance,Bonus,Gross_Total,PF,Incometax,Professiontax,SalaryAdv,Others,Deduction_Total,Total_Sal")] PayslipModelVeiw PayslipModelVeiw)
        {
            string employeeid = Request.Form["Name"].ToString();
            //if (ModelState.IsValid)
            //{
                //string query = "UPDATE Payslip SET Basic = @Basic, HRA = @HRA,DA=@DA,Coveyance=@Coveyance,Bonus=@Bonus Where PId =@PId";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePayslipDetails"))
                    {
                        cmd.Connection = con;                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Month", PayslipModelVeiw.Month);
                    cmd.Parameters.AddWithValue("@EmployeeId", PayslipModelVeiw.EmployeeId);                   
                    cmd.Parameters.AddWithValue("@Basic", PayslipModelVeiw.Basic);
                        cmd.Parameters.AddWithValue("@HRA", PayslipModelVeiw.HRA);
                        cmd.Parameters.AddWithValue("@DA", PayslipModelVeiw.DA);
                        cmd.Parameters.AddWithValue("@Coveyance", PayslipModelVeiw.Coveyance);                    
                        cmd.Parameters.AddWithValue("@Bonus", PayslipModelVeiw.Bonus);
                        cmd.Parameters.AddWithValue("@Gross_Total", PayslipModelVeiw.Gross_Total);
                        cmd.Parameters.AddWithValue("@PF", PayslipModelVeiw.PF);
                        cmd.Parameters.AddWithValue("@Incometax", PayslipModelVeiw.Incometax);
                        cmd.Parameters.AddWithValue("@Professiontax", PayslipModelVeiw.Professiontax);
                        cmd.Parameters.AddWithValue("@SalaryAdv", PayslipModelVeiw.SalaryAdv);
                        cmd.Parameters.AddWithValue("@Others", PayslipModelVeiw.Others);
                        cmd.Parameters.AddWithValue("@Deduction_Total", PayslipModelVeiw.Deduction_Total);
                        cmd.Parameters.AddWithValue("@Total_Sal", PayslipModelVeiw.Total_Sal);                       
                        con.Open();                     
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();                        
                    }
                }                
            return View(PayslipModelVeiw);
        }


        // 3. *************Delete Payslip ******************

        // GET: Home/DeletePayslip/5
        public ActionResult DeletePayslip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayslipModelVeiw Payslip = new PayslipModelVeiw();
            string query = "SELECT * FROM Payslip where EmployeeId=" + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Payslip = new PayslipModelVeiw
                            {
                                PId = Convert.ToInt32(sdr["PId"]),
                                Basic = Convert.ToString(sdr["Basic"]),
                                HRA = Convert.ToString(sdr["HRA"]),
                                DA = Convert.ToString(sdr["DA"]),
                                Coveyance = Convert.ToString(sdr["Coveyance"]),
                                Bonus = Convert.ToString(sdr["Bonus"])                                
                            };
                        }
                    }
                    con.Close();
                }
            }
            return View(Payslip);
        }

        // POST: Home/DeletePayslip/5
        [HttpPost, ActionName("DeletePayslip")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePayslip(int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "Delete FROM Pasylip where EmployeeId='" + id + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("Index");
        }


    }
}
    
