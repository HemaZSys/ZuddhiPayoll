using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Net;

namespace First.Controllers
{
    public class PayslipEntryController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;

        // GET: PayslipEntry
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
           // ViewBag.Gradelist = GetLookup("Grade");
            return View();            
        }
        [HandleError]
        [HttpGet]
        public ActionResult PayslipGradeEntryview(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.Gradelist = GetLookup("Grade");

            //string Grade = Request.Form["Grade"].ToString();

            string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.ToList();

                if (employees != null)
                {
                    ViewBag.employees = employees;
                }
            }
            
            //PayslipEntry

            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

           

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_payslip_generateempidmonth"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
                            payslipGradeEntry.EmployeeName = Convert.ToString(sdr["Name"]);
                            payslipGradeEntry.EmployeeGrade = Convert.ToString(sdr["GradeName"] );
                            payslipGradeHeader.EmployeeGradeID = Convert.ToString(sdr["Grade"]);
                            payslipGradeEntry.EmployeeGrosspay = Convert.ToDecimal(sdr["Grosspay"]);

                            payslipGradeEntry.Location = Convert.ToString(sdr["Location"]);
                            payslipGradeEntry.Department = Convert.ToString(sdr["Department"]);
                            payslipGradeEntry.Designation = Convert.ToString(sdr["Designation"]);

                            payslipGradeEntry.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            payslipGradeEntry.DOJ = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]);
                            payslipGradeEntry.DOC = Convert.ToDateTime(sdr["DateofConfirmation"] == DBNull.Value ? DateTime.Now : sdr["DateofConfirmation"]);
                            payslipGradeEntry.SectionDescription = Convert.ToString(sdr["SectionDescription"]);
                            payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
                            if(sdr["Percentage"].ToString() != "")
                            { 
                                payslipGradeEntry.Percentage = Convert.ToInt32(sdr["Percentage"]);
                            }
                            payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
                            payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
                            payslipGradeEntry.LOP = Convert.ToDecimal(sdr["LOP"]);
                            payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
                            payslipGradeEntry.EmployeeId = Convert.ToString(sdr["EmployeeId"]);
                            payslipGradeHeader.TaxCollectedfromRegime = Convert.ToDecimal(sdr["TaxCollected"]);
                            payslipGradeHeader.OldTaxProjection = Convert.ToDecimal(sdr["OldTaxProjection"]); 
                            payslipGradeHeader.NewTaxProjection = Convert.ToDecimal(sdr["NewTaxProjection"]); 
                            payslipGradeHeader.OldregimeOrNewregime = Convert.ToDecimal(sdr["OldregimeOrNewregime"]);



                            payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
                        }
                    }
                    con.Close();
                }
                //history
                List<PayslipGradeEntry>  PayslipGradeEntryListHistory = new List<PayslipGradeEntry>();



                
                    using (SqlCommand cmd = new SqlCommand("sp_payslip_generateempidmonthhistory"))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@empid", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
                                payslipGradeEntry.EmployeeName = Convert.ToString(sdr["Name"]);
                                payslipGradeEntry.EmployeeGrade = Convert.ToString(sdr["GradeName"]);
                                payslipGradeEntry.EmployeeGrosspay = Convert.ToDecimal(sdr["Grosspay"]);
                                payslipGradeEntry.Location = Convert.ToString(sdr["Location"]);
                                payslipGradeEntry.Department = Convert.ToString(sdr["Department"]);
                                payslipGradeEntry.Designation = Convert.ToString(sdr["Designation"]);

                                payslipGradeEntry.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                                payslipGradeEntry.DOJ = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]);
                            payslipGradeEntry.DOC = Convert.ToDateTime(sdr["DateofConfirmation"] == DBNull.Value ? DateTime.Now : sdr["DateofConfirmation"]);

                            payslipGradeEntry.SectionDescription = Convert.ToString(sdr["SectionDescription"]);

                                payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
                                if (sdr["Percentage"].ToString() != "")
                                {
                                    payslipGradeEntry.Percentage = Convert.ToInt32(sdr["Percentage"]);
                                }
                                if(sdr["MonthlyAmount"] != null)
                                payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
                            if (sdr["MonthYear"] != System.DBNull.Value)
                                payslipGradeEntry.MonthYear = Convert.ToDateTime(sdr["MonthYear"]);
                                payslipGradeEntry.LOP = Convert.ToDecimal(sdr["LOP"]);
                                payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
                                payslipGradeEntry.EmployeeId = Convert.ToString(sdr["EmployeeId"]);

                                PayslipGradeEntryListHistory.Add(payslipGradeEntry);

                            }
                        }
                        con.Close();
                    }
                ViewBag.PayslipGradeEntryListHistory = PayslipGradeEntryListHistory;
                    //history end

                //string query2 = "select isnull(sum(pe.MonthlyAmount),0) as taxcollected " +
                //                 " from [dbo].[Emp] e " +
                //                 " left join[dbo].[PayslipEntry] pe on pe.EmployeeId = e.Id " +
                //                 " left join[dbo].[PayslipGrade] pg on pg.Id = pe.PayslipGradeid " +
                //                 " where pg.Description = 'IT' and e.id = @empid and YEAR(pe.MonthYear) = YEAR(@month)";

                using (SqlCommand cmd = new SqlCommand("sp_taxcollectedtillnow", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Parameters.AddWithValue("@month", DateTime.Now);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {                            
                            payslipGradeHeader.taxcollected = Convert.ToDecimal(sdr["taxcollected"]);
                        }
                    }
                    con.Close();
                }

                if (payslipGradeHeader.PayslipGradeEntryList.Count == 0)
                {
                    payslipGradeHeader.PayslipGradeEntryList.Add(new PayslipGradeEntry());
                }
                else
                {
                    payslipGradeHeader.EmpId = Convert.ToInt32(id);
                    payslipGradeHeader.EmployeeId = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeId;
                    // payslipGradeHeader.EmployeeGrade = payslipGradeHeader.PayslipGradeList.First().GradeName;
                    payslipGradeHeader.EmployeeName = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeName;
                    payslipGradeHeader.EmployeeGrade = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrade;
                    payslipGradeHeader.EmployeeGrosspay = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrosspay.ToString();

                    payslipGradeHeader.Department = payslipGradeHeader.PayslipGradeEntryList.First().Department;
                    payslipGradeHeader.Designation = payslipGradeHeader.PayslipGradeEntryList.First().Designation;
                    payslipGradeHeader.Location = payslipGradeHeader.PayslipGradeEntryList.First().Location;
                    payslipGradeHeader.PFAccountNo = payslipGradeHeader.PayslipGradeEntryList.First().PFAccountNo;
                    payslipGradeHeader.DOJ = payslipGradeHeader.PayslipGradeEntryList.First().DOJ;
                    payslipGradeHeader.DOC = payslipGradeHeader.PayslipGradeEntryList.First().DOC;
                }
                return View(payslipGradeHeader);
            }
        }
        [HandleError]
        [HttpPost]
        public ActionResult PayslipGradeEntryview(PayslipGradeHeader payslipGradeHeader)
        {
            int headerid = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Payslip header data into database                
                using (SqlCommand cmd = new SqlCommand("sp_PayslipHeader_create", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    ///////
                    cmd.Parameters.AddWithValue("@Id", payslipGradeHeader.PayslipHeaderid);
                    cmd.Parameters.AddWithValue("@EmpId", payslipGradeHeader.EmpId);
                    if (payslipGradeHeader.MonthYear.ToString() != "01-01-0001 00:00:00")
                        cmd.Parameters.AddWithValue("@Month", payslipGradeHeader.MonthYear);
                    else
                    {
                        DateTime curDt = DateTime.Now;
                        cmd.Parameters.AddWithValue("@Month", curDt);
                    }
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", Convert.ToInt32(Session["Id"]));
                    cmd.Parameters.AddWithValue("@LOP", payslipGradeHeader.LOP);
                    cmd.Parameters.AddWithValue("@LeavesTaken", payslipGradeHeader.LeavesTaken);
                    cmd.Parameters.AddWithValue("@PaymentMode", payslipGradeHeader.PaymentModeid);
                    cmd.Parameters.AddWithValue("@NetSalary", payslipGradeHeader.NetSalary);
                    cmd.Parameters.AddWithValue("@GrossSalary", payslipGradeHeader.GrossSalary);
                    cmd.Parameters.AddWithValue("@TotalDeductions", payslipGradeHeader.TotalDeductions);
                    cmd.Parameters.AddWithValue("@isSalaryStructure", payslipGradeHeader.isSalaryStructure);
                    cmd.Parameters.AddWithValue("@Grade", payslipGradeHeader.EmployeeGradeID);
                    ///////
                    con.Open();
                    headerid = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Payslip data into database                
                using (SqlCommand cmd = new SqlCommand("SP_PayslipEntry_Create", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (PayslipGradeEntry payslipGradeEntry in payslipGradeHeader.PayslipGradeEntryList)
                    {
                        cmd.Parameters.Clear();

                        if (payslipGradeHeader.MonthYear.ToString() != "01-01-0001 00:00:00")
                            cmd.Parameters.AddWithValue("@MonthYear", payslipGradeHeader.MonthYear);
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@MonthYear", curDt);
                        }
                        cmd.Parameters.AddWithValue("@Id", payslipGradeEntry.Id);
                        cmd.Parameters.AddWithValue("@EmployeeId", payslipGradeHeader.EmpId);
                        cmd.Parameters.AddWithValue("@MonthlyAmount", payslipGradeEntry.MonthlyAmount);
                        cmd.Parameters.AddWithValue("@AnnualAmount", payslipGradeEntry.AnnualAmount);
                        cmd.Parameters.AddWithValue("@LOP", payslipGradeHeader.LOP);
                        cmd.Parameters.AddWithValue("@PayslipGradeid", payslipGradeEntry.PayslipGradeid);
                        cmd.Parameters.AddWithValue("@PayslipHeaderid", headerid);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteScalar();
                        con.Close();
                    }
                }
            }

            
            return RedirectToAction("Details", "Employee");
        }
        [HandleError]
        public List<Empalldropdown> GetLookup(string type)
        {
            List<Empalldropdown> Empalldropdownlist = new List<Empalldropdown>();
            string query = "SELECT * FROM Empalldropdown where type = '" + type + "'";
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
                            Empalldropdownlist.Add(new Empalldropdown
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Type = Convert.ToString(sdr["type"]),
                                Abbreviation = Convert.ToString(sdr["abbreviation"]),
                                Description = Convert.ToString(sdr["description"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            if (Empalldropdownlist.Count == 0)
            {
                Empalldropdownlist.Add(new Empalldropdown());
            }
            return Empalldropdownlist;
        }
        [HandleError]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
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

        /// <summary>
        /// Payslip Structure Create
        /// </summary>
        /// <returns></returns>
         [HandleError]
        [HttpGet]     
        public ActionResult Payslipstructurecreate()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult Payslipstructurecreate(Empalldropdown e)
        {
            if (Request.HttpMethod == "POST")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Empalldropdown values (@type,@abbreviation,@description)", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("@Id", e.Id);
                        cmd.Parameters.AddWithValue("@type", e.Type);
                        cmd.Parameters.AddWithValue("@abbreviation", e.Abbreviation);
                        cmd.Parameters.AddWithValue("@description", e.Description);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return RedirectToAction("Dropdownsettingslist", "Employee");
        }

        //For Export Payslip Report
        public ActionResult PayslipReport(int? id)
        {           
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<PayslipGradeHeader> payslipGradeHeaderList = new List<PayslipGradeHeader>();

            PaySlipReport oPaySlipReport = new PaySlipReport();
            string payslip = "sp_payslip_PivotReport";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {
                   
                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@id", id);

                    con2.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            oReportRow = new ReportRow();
                            for (int i = 0; i < sdr.FieldCount; i++)
                            {
                                ReportField oReportField = new ReportField();
                                oReportField.FieldName = sdr.GetName(i);                               
                                oReportField.FieldValue = Convert.ToString(sdr.GetValue(i));                                
                                oReportRow.ReportFieldList.Add(oReportField);
                            }
                            oPaySlipReport.ReportRowList.Add(oReportRow);
                        }
                    }
                    con2.Close();

                    
                }
            }
            //ViewBag.payslips = payslipGradeHeaderList;

            return View(oPaySlipReport);
        }       

    }
}