using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Web.Security;


namespace First.Controllers
{
    public class GradeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
        // GET: Grade
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult CreatePayslipGrade()
        //{
        //    return View();
        //}

        // 2. *************ADD NEW Grade Details ******************
        [HandleError]
        [HttpPost]
        public ActionResult CreatePayslipGrade(PayslipGrade p)
        {
            if (Request.HttpMethod == "POST")
            {

                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("SP_PayslipGrade_Create", con))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", p.Id);
                        cmd.Parameters.AddWithValue("@GradeName", p.GradeName);
                        cmd.Parameters.AddWithValue("@LineNumber", p.LineNumber);
                        cmd.Parameters.AddWithValue("@SequenceNumber", p.SequenceNumber);
                        cmd.Parameters.AddWithValue("@SectionDescription", p.SectionDescription);
                        cmd.Parameters.AddWithValue("@Description", p.Description);
                        cmd.Parameters.AddWithValue("@Percentage", p.Percentage);
                        cmd.Parameters.AddWithValue("@EarningOrDeduction", p.EarningOrDeduction);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            //return View();
            return RedirectToAction("GradeDetails", "Grade");
        }
        // 1. *************show the list of Grade Details ******************
        // GET: Home
        [HandleError]
        public ActionResult GradeDetails()
        {
            List<PayslipGrade> Payslipgrades = new List<PayslipGrade>();
            string query = "SELECT * FROM PayslipGrade ORDER BY GradeName, SequenceNumber";
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
                            Payslipgrades.Add(new PayslipGrade
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                GradeName = Convert.ToString(sdr["GradeName"]),
                                LineNumber = Convert.ToInt32(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
                                SectionDescription = Convert.ToString(sdr["SectionDescription"]),
                                Description = Convert.ToString(sdr["Description"]),
                                Percentage = Convert.ToDecimal(sdr["Percentage"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (Payslipgrades.Count == 0)
            {
                Payslipgrades.Add(new PayslipGrade());
            }
            return View(Payslipgrades);
        }

        // GET: Home/EmployeeDetails/5
        [HandleError]
        public ActionResult PayslipGradeDetails(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayslipGrade grade = new PayslipGrade();
            string query = "SELECT * FROM PayslipGrade where Id=" + id;
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
                            grade = new PayslipGrade
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                GradeName = Convert.ToString(sdr["GradeName"]),
                                LineNumber = Convert.ToInt32(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToInt32(sdr["SequenceNumber"]),
                                SectionDescription = Convert.ToString(sdr["SectionDescription"]),
                                Description = Convert.ToString(sdr["Description"]),
                                Percentage = Convert.ToDecimal(sdr["Percentage"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }
        // 3. *************Update Grade Detail ******************

        // GET: Home/UpdatePayslipGrade/5
        [HandleError]
        public ActionResult CreatePayslipGrade(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == 0)
            {
                return View();
            }
            PayslipGrade grade = new PayslipGrade();
            string query = "SELECT * FROM PayslipGrade where Id=" + id;
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
                            grade = new PayslipGrade
                            {

                                Id = Convert.ToInt32(sdr["Id"]),
                                GradeName = Convert.ToString(sdr["GradeName"]),
                                LineNumber = Convert.ToInt32(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
                                SectionDescription = Convert.ToString(sdr["SectionDescription"]),
                                Description = Convert.ToString(sdr["Description"]),
                                Percentage = Convert.ToDecimal(sdr["Percentage"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Home/UpdateEmployee/5        
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePayslipGrade([Bind(Include = "Id,GradeName,LineNumber,SequenceNumber,SectionDescription,Description,Percentage,EarningOrDeduction")] PayslipGrade payslipGrade)
        {
            //if (ModelState.IsValid)
            //{
            string query = "UPDATE PayslipGrade SET GradeName = @GradeName, LineNumber = @LineNumber, SequenceNumber = @SequenceNumber,SectionDescription=@SectionDescription,Description = @Description,Percentage=@Percentage,EarningOrDeduction=@EarningOrDeduction Where Id =@Id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", payslipGrade.Id);
                    cmd.Parameters.AddWithValue("@GradeName", payslipGrade.GradeName);
                    cmd.Parameters.AddWithValue("@LineNumber", payslipGrade.LineNumber);
                    cmd.Parameters.AddWithValue("@SequenceNumber", payslipGrade.SequenceNumber);
                    cmd.Parameters.AddWithValue("@SectionDescription", payslipGrade.SectionDescription);
                    cmd.Parameters.AddWithValue("@Description", payslipGrade.Description);
                    cmd.Parameters.AddWithValue("@Percentage", payslipGrade.Percentage);
                    cmd.Parameters.AddWithValue("@EarningOrDeduction", payslipGrade.EarningOrDeduction);
                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            // return RedirectToAction("Index");
            //}
            //return View(payslipGrade);
            return RedirectToAction("GradeDetails", "Grade");
        }

        // 3. *************Delete Payslip Grade ******************

        //// GET: Home/DeletePayslipGrade/5
        //public ActionResult DeletePayslipGrade(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PayslipGrade grade = new PayslipGrade();
        //    string query = "SELECT * FROM PayslipGrade where Id=" + id;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    grade = new PayslipGrade
        //                    {
        //                        Id = Convert.ToInt32(sdr["Id"]),
        //                        GradeName = Convert.ToString(sdr["GradeName"]),
        //                        LineNumber = Convert.ToInt32(sdr["LineNumber"]),
        //                        SequenceNumber = Convert.ToInt32(sdr["SequenceNumber"]),
        //                        SectionDescription = Convert.ToString(sdr["SectionDescription"]),
        //                        Description = Convert.ToString(sdr["Description"]),
        //                        Percentage = Convert.ToDecimal(sdr["Percentage"]),
        //                        EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
        //                    };
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return View(grade);
        //}

        // POST: Home/DeleteEmployee/5
        [HandleError]
        [HttpGet, ActionName("DeletePayslipGrade")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeletePayslipGrade(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "Delete FROM PayslipGrade where Id='" + id + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ViewBag.AlertMsg = "Record Deleted Successfully";
                }
                return RedirectToAction("GradeDetails");
            }
            catch
            {
                return View();
            }
        }
        [HandleError]
        public ActionResult Logout()
        {

            Session["Email"] = "";
            Session["Id"] = "";
            Session["AccessType"] = "";
            Session.Abandon();
            //disable browsers back buttons.

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));

            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "UserLogin");

        }
    }
}