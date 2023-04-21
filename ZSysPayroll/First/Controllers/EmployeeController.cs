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
using Rotativa;
using System.Web.Security;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using HtmlAgilityPack;
using System.Text;
using System.Net.Mail;

namespace First.Controllers
{
    public class EmployeeController : Controller
    {
        //AppEntities ctx;
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
        // GET: Enrollment
        public string value = "";
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    Employee emp = new Employee();
        //    AppEntities1 db = new AppEntities1();
        //    //sort the employee and get the last insert employee.
        //    var lastemployee = db.Emps.OrderByDescending(c => c.EmployeeId).FirstOrDefault();
        //    if (lastemployee == null)
        //    {
        //        emp.EmployeeId = "ZS0001";
        //    }
        //    else
        //    {
        //        //using string substring method to get the number of the last inserted employee's EmployeeID 
        //        emp.EmployeeId = "ZS" + (Convert.ToInt32(lastemployee.EmployeeId.Substring(2, lastemployee.EmployeeId.Length - 2)) + 1).ToString("D4");
        //    }

        //    ViewBag.Designationlist = GetLookup("Designation");
        //    ViewBag.BloodGrouplist = GetLookup("BloodGroup");
        //    ViewBag.Departmentlist = GetLookup("Department");
        //    ViewBag.Qualificationlist = GetLookup("Qualification");
        //    ViewBag5list = GetLookup("Grade");           
        //    return View(emp);
        //}

        // 2. *************ADD NEW Employee Details ******************
        //[HandleError]
        [HttpPost]
        public ActionResult Index(Employee e)
        {
            if (Request.HttpMethod == "POST")
            {
                //Employee er = new Employee();

                /*
                string Department = Request.Form["Department"].ToString();
                string Designation = Request.Form["Designation"].ToString();
                string BloodGroup = Request.Form["Bloodgroup"].ToString();
                string Qualification = Request.Form["Qualification"].ToString();
                string Grade = Request.Form["Grade"].ToString();
                */

                using (SqlConnection con = new SqlConnection(constr))
                {
                    //string query1 = " []";
                    using (SqlCommand cmd1 = new SqlCommand("SP_Employee_Access", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        cmd1.Parameters.AddWithValue("@Email", e.offcEmail == null ? "" : e.offcEmail);
                        cmd1.Parameters.AddWithValue("@AccessType", e.AccessType == null ? "" : e.AccessType);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand("SP_Employee", con))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", e.Id);
                        cmd.Parameters.AddWithValue("@Name", e.Name);
                        cmd.Parameters.AddWithValue("@Designation", e.Designation);
                        cmd.Parameters.AddWithValue("@EmployeeId", e.EmployeeId);
                        DateTime a;

                        if (e.DateofJoin.Year > 1753 && e.DateofJoin.Year < 9999)
                        {
                            cmd.Parameters.AddWithValue("@DateofJoin", e.DateofJoin);
                        }
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DateofJoin", curDt);
                        }

                        if (e.DateofRelieving.Year > 1753 && e.DateofRelieving.Year < 9999)
                        {
                            cmd.Parameters.AddWithValue("@DateofRelieving", e.DateofRelieving);
                        }
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DateofRelieving", curDt);
                        }

                        if (e.DateofConfirmation.Year > 1753 && e.DateofConfirmation.Year < 9999)
                        {
                            cmd.Parameters.AddWithValue("@DateofConfirmation", e.DateofConfirmation);
                        }
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DateofConfirmation", curDt);
                        }

                        cmd.Parameters.AddWithValue("@Gender", e.Gender);
                        cmd.Parameters.AddWithValue("@Qualification", e.Qualification);
                        cmd.Parameters.AddWithValue("@Address", e.Address);
                        cmd.Parameters.AddWithValue("@Contact", e.Contact);
                        cmd.Parameters.AddWithValue("@PAN", e.PAN);
                        cmd.Parameters.AddWithValue("@PFAccountNo", e.PFAccountNo);
                        cmd.Parameters.AddWithValue("@Aadhar", e.Aadhar);
                        cmd.Parameters.AddWithValue("@Passport", e.Passport);

                        if (e.DOB.Year > 1753 && e.DOB.Year < 9999)
                        {
                            cmd.Parameters.AddWithValue("@DOB", e.DOB);
                        }
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DOB", curDt);
                        }

                        cmd.Parameters.AddWithValue("@Grade", e.Grade);
                        cmd.Parameters.AddWithValue("@Grosspay", e.Grosspay);
                        cmd.Parameters.AddWithValue("@Age", e.Age);
                        cmd.Parameters.AddWithValue("@Email", e.Email);
                        cmd.Parameters.AddWithValue("@LName", e.LName);
                        cmd.Parameters.AddWithValue("@Alternativeno", e.Alternativeno);
                        cmd.Parameters.AddWithValue("@offcEmail", e.offcEmail);
                        cmd.Parameters.AddWithValue("@PermanentAddress", e.PermanentAddress);
                        cmd.Parameters.AddWithValue("@Department", e.Department);
                        cmd.Parameters.AddWithValue("@Empstatus", e.Empstatus);
                        cmd.Parameters.AddWithValue("@Location", e.Location);
                        cmd.Parameters.AddWithValue("@Reportingmanager", e.Reportingmanager);
                        cmd.Parameters.AddWithValue("@Precompanyname", e.Precompanyname);
                        cmd.Parameters.AddWithValue("@Predesignation", e.Predesignation);
                        cmd.Parameters.AddWithValue("@Preexperience", e.Preexperience);
                        cmd.Parameters.AddWithValue("@Bankname", e.Bankname);
                        cmd.Parameters.AddWithValue("@Bankacno", e.Bankacno);
                        cmd.Parameters.AddWithValue("@Bankbranch", e.Bankbranch);
                        cmd.Parameters.AddWithValue("@Bankifsc", e.Bankifsc);
                        cmd.Parameters.AddWithValue("@Maritalstatus", e.Maritalstatus);
                        cmd.Parameters.AddWithValue("@Bloodgroup", e.BloodGroup);
                        cmd.Parameters.AddWithValue("@Availability", e.Availability);
                        cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@modified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CompanyName", e.CompanyName);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            //return View();

            if (Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
            {
                return RedirectToAction("Details", "Employee");
            }
            else
            {
                return RedirectToAction("EmployeeDetails", "Employee", new { id = e.Id });
            }
        }

        // 1. *************show the list of Employee ******************
        // GET: Home

        [HandleError]
        [HttpGet]
        public ActionResult Details()
        {
            List<Employee> Employees = new List<Employee>();
            string query = "";
            string EmpID = "";
            EmpID = Convert.ToString(Session["Id"]);
            if (Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
            {
                query = "SELECT em.*,isnull(emgrd.description,'') as EmployeeStatus , isnull(emgr.description,'') as EmployeeGrade,rptmgr.Name +' '+ rptmgr.LName as ReportingmanagerName" +
                        Environment.NewLine + "FROM EmployeeDetails em" +
                        Environment.NewLine + "left join[Empalldropdown](nolock) emgrd on emgrd.Id = em.Empstatus" +
                        Environment.NewLine + "left join EmployeeDetails(nolock) rptmgr on em.Reportingmanager = rptmgr.Id" +
                        Environment.NewLine + "left join[Empalldropdown](nolock) emgr on emgr.Id = em.Grade" +
                        Environment.NewLine + "ORDER BY EmployeeId";
            }
            else
            {
                query = "SELECT em.*,isnull(emgrd.description,'') as EmployeeStatus FROM EmployeeDetails em left join [Empalldropdown](nolock) emgrd on emgrd.Id = em.Empstatus where id = " + EmpID + " ORDER BY EmployeeId ";
            }

            if (EmpID != "")
            {
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
                                Employees.Add(new Employee
                                {
                                    Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                    Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                    Designation = Convert.ToString(sdr["Designation"] == DBNull.Value ? "" : sdr["Designation"]),
                                    EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                    DateofJoin = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]),
                                    DateofRelieving = Convert.ToDateTime(sdr["DateofRelieving"] == DBNull.Value ? DateTime.Now : sdr["DateofRelieving"]),
                                    DateofConfirmation = Convert.ToDateTime(sdr["DateofConfirmation"] == DBNull.Value ? DateTime.Now : sdr["DateofConfirmation"]),
                                    Gender = Convert.ToString(sdr["Gender"] == DBNull.Value ? "" : sdr["Gender"]),
                                    Qualification = Convert.ToString(sdr["Qualification"] == DBNull.Value ? "" : sdr["Qualification"]),
                                    Address = Convert.ToString(sdr["Address"] == DBNull.Value ? "" : sdr["Address"]),
                                    Contact = Convert.ToString(sdr["Contact"] == DBNull.Value ? "" : sdr["Contact"]),
                                    PAN = Convert.ToString(sdr["PAN"] == DBNull.Value ? "" : sdr["PAN"]),
                                    PFAccountNo = Convert.ToString(sdr["PFAccountNo"] == DBNull.Value ? "" : sdr["PFAccountNo"]),
                                    Aadhar = Convert.ToString(sdr["Aadhar"] == DBNull.Value ? "" : sdr["Aadhar"]),
                                    Passport = Convert.ToString(sdr["Passport"] == DBNull.Value ? "" : sdr["Passport"]),
                                    DOB = Convert.ToDateTime(sdr["DOB"] == DBNull.Value ? DateTime.Now : sdr["DOB"]),
                                    Grade = Convert.ToString(sdr["EmployeeGrade"] == DBNull.Value ? "" : sdr["EmployeeGrade"]),
                                    Grosspay = Convert.ToDecimal(sdr["Grosspay"] == DBNull.Value ? 0 : sdr["Grosspay"]),
                                    Age = Convert.ToInt32(sdr["Age"] == DBNull.Value ? 0 : sdr["Age"]),
                                    Email = Convert.ToString(sdr["Email"] == DBNull.Value ? "" : sdr["Email"]),
                                    LName = Convert.ToString(sdr["LName"] == DBNull.Value ? "" : sdr["LName"]),
                                    Alternativeno = Convert.ToString(sdr["Alternativeno"] == DBNull.Value ? "" : sdr["Alternativeno"]),
                                    offcEmail = Convert.ToString(sdr["offcEmail"] == DBNull.Value ? "" : sdr["offcEmail"]),
                                    PermanentAddress = Convert.ToString(sdr["PermanentAddress"] == DBNull.Value ? "" : sdr["PermanentAddress"]),
                                    Department = Convert.ToString(sdr["Department"] == DBNull.Value ? "" : sdr["Department"]),
                                    Empstatus = Convert.ToString(sdr["EmployeeStatus"] == DBNull.Value ? "" : sdr["EmployeeStatus"]),
                                    Location = Convert.ToString(sdr["EmployeeStatus"] == DBNull.Value ? "" : sdr["Location"]),
                                    Reportingmanager = Convert.ToString(sdr["ReportingmanagerName"] == DBNull.Value ? "" : sdr["ReportingmanagerName"]),
                                    Precompanyname = Convert.ToString(sdr["Precompanyname"] == DBNull.Value ? "" : sdr["Precompanyname"]),
                                    Predesignation = Convert.ToString(sdr["Predesignation"] == DBNull.Value ? "" : sdr["Predesignation"]),
                                    Preexperience = Convert.ToString(sdr["Preexperience"] == DBNull.Value ? "" : sdr["Preexperience"]),
                                    Bankname = Convert.ToString(sdr["Bankname"] == DBNull.Value ? "" : sdr["Bankname"]),
                                    Bankacno = Convert.ToString(sdr["Bankacno"] == DBNull.Value ? "" : sdr["Bankacno"]),
                                    Bankbranch = Convert.ToString(sdr["Bankbranch"] == DBNull.Value ? "" : sdr["Bankbranch"]),
                                    Bankifsc = Convert.ToString(sdr["Bankifsc"] == DBNull.Value ? "" : sdr["Bankifsc"]),
                                    Maritalstatus = Convert.ToString(sdr["Maritalstatus"] == DBNull.Value ? "" : sdr["Maritalstatus"]),
                                    BloodGroup = Convert.ToString(sdr["Bloodgroup"] == DBNull.Value ? "" : sdr["Bloodgroup"]),
                                    Availability = Convert.ToString(sdr["Availability"] == DBNull.Value ? "" : sdr["Availability"])
                                });
                            }
                        }
                        con.Close();
                    }
                }
            }

            if (Employees.Count == 0)
            {
                Employees.Add(new Employee());
            }
            return View(Employees);
        }

        // GET: Home/EmployeeDetails/5
        [HandleError]
        public ActionResult EmployeeDetails(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<PayslipGradeHeader> payslipGradeHeaderList = new List<PayslipGradeHeader>();


            string payslip = "sp_deduction_itpaid";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@id", id);

                    con2.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["MonthYear"].ToString() != "")
                            {
                                payslipGradeHeaderList.Add(new PayslipGradeHeader
                                {

                                    EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                    EmpId = Convert.ToInt32(sdr["Id"]),
                                    EmployeeName = Convert.ToString(sdr["Name"]),
                                    MonthYear = Convert.ToDateTime(sdr["MonthYear"]),
                                    GrossSalary = Convert.ToDecimal(sdr["GrossSalary"]),
                                    TotalDeductions = Convert.ToDecimal(sdr["TotalDeductions"]),
                                    NetSalary = Convert.ToDecimal(sdr["NetSalary"]),
                                    taxcollected = Convert.ToDecimal(sdr["ITpaid"]),
                                    Ranking = Convert.ToDecimal(sdr["ranking"])

                                });
                            }
                        }
                    }
                    con2.Close();
                }
            }
            ViewBag.payslips = payslipGradeHeaderList;

            List<Form80CHeader> Form80CHeaderlist = new List<Form80CHeader>();

            string form = "Get_Employee_Taxregime";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(form))
                {
                    cmd2.Connection = con2;
                    con2.Open();
                    cmd2.Parameters.AddWithValue("@empid", id);
                    cmd2.CommandType = CommandType.StoredProcedure;


                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80CHeaderlist.Add(new Form80CHeader
                            {
                                Employeeid = Convert.ToString(sdr["EmployeeId"]),
                                Empid = Convert.ToInt32(sdr["Id"]),
                                EmployeeName = Convert.ToString(sdr["Name"]),
                                MonthYear = Convert.ToDateTime(sdr["monthyear"]),
                                Ranking = Convert.ToDecimal(sdr["ranking"]),
                                NewTaxProjection = Convert.ToDecimal(sdr["NewTaxProjection"]),
                                OldTaxProjection = Convert.ToDecimal(sdr["OldTaxProjection"]),
                                OldregimeOrNewregime = Convert.ToString(sdr["OldregimeOrNewregime"]),
                                FinancialYear = Convert.ToString(sdr["FinancialYear"]),
                                TaxCollected = Convert.ToDecimal(sdr["TaxCollected"])
                            });
                        }
                    }
                    con2.Close();
                }
            }
            ViewBag.deductions = Form80CHeaderlist;



            Employee emp = new Employee();
            string query = "Get_Individual_Employee_Detail";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            emp = new Employee
                            {
                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                Designation = Convert.ToString(sdr["DesignationName"] == DBNull.Value ? "" : sdr["DesignationName"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]),
                                DateofRelieving = Convert.ToDateTime(sdr["DateofRelieving"] == DBNull.Value ? DateTime.Now : sdr["DateofRelieving"]),
                                DateofConfirmation = Convert.ToDateTime(sdr["DateofConfirmation"] == DBNull.Value ? DateTime.Now : sdr["DateofConfirmation"]),
                                Gender = Convert.ToString(sdr["Gender"] == DBNull.Value ? "" : sdr["Gender"]),
                                Qualification = Convert.ToString(sdr["QualificationName"] == DBNull.Value ? "" : sdr["QualificationName"]),
                                Address = Convert.ToString(sdr["Address"] == DBNull.Value ? "" : sdr["Address"]),
                                Contact = Convert.ToString(sdr["Contact"] == DBNull.Value ? "" : sdr["Contact"]),
                                PAN = Convert.ToString(sdr["PAN"] == DBNull.Value ? "" : sdr["PAN"]),
                                PFAccountNo = Convert.ToString(sdr["PFAccountNo"] == DBNull.Value ? "" : sdr["PFAccountNo"]),
                                Aadhar = Convert.ToString(sdr["Aadhar"] == DBNull.Value ? "" : sdr["Aadhar"]),
                                Passport = Convert.ToString(sdr["Passport"] == DBNull.Value ? "" : sdr["Passport"]),
                                DOB = Convert.ToDateTime(sdr["DOB"] == DBNull.Value ? DateTime.Now : sdr["DOB"]),
                                Grade = Convert.ToString(sdr["Grade"] == DBNull.Value ? "" : sdr["Grade"]),
                                Grosspay = Convert.ToDecimal(sdr["Grosspay"] == DBNull.Value ? 0 : sdr["Grosspay"]),
                                Age = Convert.ToInt32(sdr["Age"] == DBNull.Value ? 0 : sdr["Age"]),
                                Email = Convert.ToString(sdr["Email"] == DBNull.Value ? "" : sdr["Email"]),
                                LName = Convert.ToString(sdr["LName"] == DBNull.Value ? "" : sdr["LName"]),
                                Alternativeno = Convert.ToString(sdr["Alternativeno"] == DBNull.Value ? 0 : sdr["Alternativeno"]),
                                offcEmail = Convert.ToString(sdr["offcEmail"] == DBNull.Value ? "" : sdr["offcEmail"]),
                                PermanentAddress = Convert.ToString(sdr["PermanentAddress"] == DBNull.Value ? "" : sdr["PermanentAddress"]),
                                Department = Convert.ToString(sdr["DepartmentName"] == DBNull.Value ? "" : sdr["DepartmentName"]),
                                Empstatus = Convert.ToString(sdr["Empstatus"] == DBNull.Value ? "" : sdr["Empstatus"]),
                                Location = Convert.ToString(sdr["Location"] == DBNull.Value ? "" : sdr["Location"]),
                                Reportingmanager = Convert.ToString(sdr["Reportingmanager"] == DBNull.Value ? "" : sdr["Reportingmanager"]),
                                Precompanyname = Convert.ToString(sdr["Precompanyname"] == DBNull.Value ? "" : sdr["Precompanyname"]),
                                Predesignation = Convert.ToString(sdr["Id"] == DBNull.Value ? "" : sdr["Predesignation"]),
                                Preexperience = Convert.ToString(sdr["Predesignation"] == DBNull.Value ? "" : sdr["Preexperience"]),
                                Bankname = Convert.ToString(sdr["Bankname"] == DBNull.Value ? "" : sdr["Bankname"]),
                                Bankacno = Convert.ToString(sdr["Bankacno"] == DBNull.Value ? "" : sdr["Bankacno"]),
                                Bankbranch = Convert.ToString(sdr["Bankbranch"] == DBNull.Value ? "" : sdr["Bankbranch"]),
                                Bankifsc = Convert.ToString(sdr["Bankifsc"] == DBNull.Value ? "" : sdr["Bankifsc"]),
                                Maritalstatus = Convert.ToString(sdr["Maritalstatus"] == DBNull.Value ? "" : sdr["Maritalstatus"]),
                                BloodGroup = Convert.ToString(sdr["BloodgroupName"] == DBNull.Value ? "" : sdr["BloodgroupName"]),
                                CompanyName = Convert.ToString(sdr["CompanyName"] == DBNull.Value ? "" : sdr["CompanyName"]),
                                Availability = Convert.ToString(sdr["Availability"] == DBNull.Value ? "" : sdr["Availability"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }

        // 3. *************Update Employee Detail ******************

        // GET: Home/UpdateEmployee/5
        [HandleError]
        public ActionResult Index(int? id, string mode)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Designationlist = GetLookup("Designation");
            ViewBag.BloodGrouplist = GetLookup("Bloodgroup");
            ViewBag.Departmentlist = GetLookup("Department");
            ViewBag.Qualificationlist = GetLookup("Qualification");
            ViewBag.Gradelist = GetLookup("Grade");
            ViewBag.Empstatuslist = GetLookup("EmployeeStatus");
            ViewBag.Locationlist = GetLookup("Location");
            ViewBag.CompanyNameList = GetLookup("CompanyName");
            ViewBag.AllEmployeeList = GetAllEmployeeList();
            Employee emp = new Employee();
            if (id == 0)
            {
                //Employee emp = new Employee();
                AppEntities1 db = new AppEntities1();
                //sort the employee and get the last insert employee.
                //var lastemployee = db.Emps.OrderByDescending(c => c.EmployeeId).FirstOrDefault();
                //var lastemployee = db.Emps.OrderByDescending(w => !w.EmployeeId.Contains("ZSPL")).FirstOrDefault();
                var query1 = "select top 1 EmployeeId from [dbo].[EmployeeDetails] where EmployeeId not like '%PL%' order by EmployeeId desc";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query1))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                emp = new Employee
                                {
                                    EmployeeId = Convert.ToString(sdr["EmployeeId"])

                                };
                            }
                        }
                        con.Close();
                    }
                }
                if (emp.EmployeeId == null)
                {
                    emp.EmployeeId = "ZS001";
                }
                else
                {
                    //using string substring method to get the number of the last inserted employee's EmployeeID 
                    //emp.EmployeeId = "ZS" + (Convert.ToInt32(lastemployee.EmployeeId.Substring(2, lastemployee.EmployeeId.Length - 2)) + 1).ToString("D3");
                    //emp.EmployeeId = "ZS" + (Convert.ToInt32(lastemployee.Substring(2, lastemployee.Length - 2)) + 1).ToString("D3");           
                    emp.EmployeeId = "ZS" + (Convert.ToInt32(emp.EmployeeId.Substring(2, emp.EmployeeId.Length - 2)) + 1).ToString("D3");

                }
                emp.DateofConfirmation = DateTime.Now;
                emp.DateofJoin = DateTime.Now;
                emp.DateofRelieving = DateTime.Now;
                emp.DOB = DateTime.Now;
                return View(emp);
            }

            string query = "SELECT emp.*,enr.accesstype FROM EmployeeDetails emp LEFT JOIN Enrollment enr on enr.email=emp.offcEmail where emp.Id=" + id;
            //string Department = Request.Form["Department"].ToString();
            //string Designation = Request.Form["Designation"].ToString();
            //string Bloodgroup = Request.Form["Bloodgroup"].ToString();
            //string Qualification = Request.Form["Qualification"].ToString();

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
                            emp = new Employee
                            {

                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                Designation = Convert.ToString(sdr["Designation"] == DBNull.Value ? "" : sdr["Designation"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]),
                                DateofRelieving = Convert.ToDateTime(sdr["DateofRelieving"] == DBNull.Value ? DateTime.Now : sdr["DateofRelieving"]),
                                DateofConfirmation = Convert.ToDateTime(sdr["DateofConfirmation"] == DBNull.Value ? DateTime.Now : sdr["DateofConfirmation"]),
                                Gender = Convert.ToString(sdr["Gender"] == DBNull.Value ? "" : sdr["Gender"]),
                                Qualification = Convert.ToString(sdr["Qualification"] == DBNull.Value ? "" : sdr["Qualification"]),
                                Address = Convert.ToString(sdr["Address"] == DBNull.Value ? "" : sdr["Address"]),
                                Contact = Convert.ToString(sdr["Contact"] == DBNull.Value ? "" : sdr["Contact"]),
                                PAN = Convert.ToString(sdr["PAN"] == DBNull.Value ? "" : sdr["PAN"]),
                                PFAccountNo = Convert.ToString(sdr["PFAccountNo"] == DBNull.Value ? "" : sdr["PFAccountNo"]),
                                Aadhar = Convert.ToString(sdr["Aadhar"] == DBNull.Value ? "" : sdr["Aadhar"]),
                                Passport = Convert.ToString(sdr["Passport"] == DBNull.Value ? "" : sdr["Passport"]),
                                DOB = Convert.ToDateTime(sdr["DOB"] == DBNull.Value ? DateTime.Now : sdr["DOB"]),
                                Grade = Convert.ToString(sdr["Grade"] == DBNull.Value ? "" : sdr["Grade"]),
                                Grosspay = Convert.ToDecimal(sdr["Grosspay"] == DBNull.Value ? 0 : sdr["Grosspay"]),
                                Age = Convert.ToInt32(sdr["Age"] == DBNull.Value ? 0 : sdr["Age"]),
                                Email = Convert.ToString(sdr["Email"] == DBNull.Value ? "" : sdr["Email"]),
                                LName = Convert.ToString(sdr["LName"] == DBNull.Value ? "" : sdr["LName"]),
                                Alternativeno = Convert.ToString(sdr["Alternativeno"] == DBNull.Value ? "" : sdr["Alternativeno"]),
                                offcEmail = Convert.ToString(sdr["offcEmail"] == DBNull.Value ? "" : sdr["offcEmail"]),
                                PermanentAddress = Convert.ToString(sdr["PermanentAddress"] == DBNull.Value ? "" : sdr["PermanentAddress"]),
                                Department = Convert.ToString(sdr["Department"] == DBNull.Value ? "" : sdr["Department"]),
                                Empstatus = Convert.ToString(sdr["Empstatus"] == DBNull.Value ? "" : sdr["Empstatus"]),
                                Location = Convert.ToString(sdr["Location"] == DBNull.Value ? "" : sdr["Location"]),
                                Reportingmanager = Convert.ToString(sdr["Reportingmanager"] == DBNull.Value ? "" : sdr["Reportingmanager"]),
                                Precompanyname = Convert.ToString(sdr["Precompanyname"] == DBNull.Value ? "" : sdr["Precompanyname"]),
                                Predesignation = Convert.ToString(sdr["Predesignation"] == DBNull.Value ? "" : sdr["Predesignation"]),
                                Preexperience = Convert.ToString(sdr["Preexperience"] == DBNull.Value ? "" : sdr["Preexperience"]),
                                Bankname = Convert.ToString(sdr["Bankname"] == DBNull.Value ? "" : sdr["Bankname"]),
                                Bankacno = Convert.ToString(sdr["Bankacno"] == DBNull.Value ? "" : sdr["Bankacno"]),
                                Bankbranch = Convert.ToString(sdr["Bankbranch"] == DBNull.Value ? "" : sdr["Bankbranch"]),
                                Bankifsc = Convert.ToString(sdr["Bankifsc"] == DBNull.Value ? "" : sdr["Bankifsc"]),
                                Maritalstatus = Convert.ToString(sdr["Maritalstatus"] == DBNull.Value ? "" : sdr["Maritalstatus"]),
                                BloodGroup = Convert.ToString(sdr["Bloodgroup"] == DBNull.Value ? "" : sdr["Bloodgroup"]),
                                Availability = Convert.ToString(sdr["Availability"] == DBNull.Value ? "" : sdr["Availability"]),
                                AccessType = Convert.ToString(sdr["AccessType"] == DBNull.Value ? "" : sdr["AccessType"]),
                                CompanyName = Convert.ToString(sdr["CompanyName"] == DBNull.Value ? "" : sdr["CompanyName"])

                            };
                        }
                    }
                    con.Close();
                }
            }
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);

        }
        private List<Empalldropdown> GetAllEmployeeList()
        {
            List<Empalldropdown> Empalldropdownlist = new List<Empalldropdown>();
            string query = "SELECT [Id],[Name] +' '+ [LName] as Employeename, [EmployeeId]  FROM [dbo].[EmployeeDetails]";
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
                                Abbreviation = Convert.ToString(sdr["EmployeeId"]),
                                Description = Convert.ToString(sdr["Employeename"])
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

        // POST: Home/UpdateEmployee/5  
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee([Bind(Include = "Id,Name,Designation,EmployeeId,DateofJoin,DateofRelieving,Gender,Qualification,Address,Contact,PAN,PFAccountNo,Aadhar,Passport,DOB,Grade,Grosspay,Age,Email,LName,Alternativeno,offcEmail,PermanentAddress,Department,Empstatus,Reportingmanager,Precompanyname,Predesignation,Preexperience,Bankname,Bankacno,Bankbranch,Bankifsc,Maritalstatus,Bloodgroup,Availability,modified")] Employee Employee)
        {
            //if (ModelState.IsValid)
            //{

            string query = " [SP_Employee_update]";
            using (SqlConnection con = new SqlConnection(constr))
            {


                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", Employee.Id);
                    cmd.Parameters.AddWithValue("@Name", Employee.Name);
                    cmd.Parameters.AddWithValue("@Designation", Employee.Designation);
                    cmd.Parameters.AddWithValue("@EmployeeId", Employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@DateofJoin", Employee.DateofJoin);
                    cmd.Parameters.AddWithValue("@DateofRelieving", Employee.DateofRelieving);
                    cmd.Parameters.AddWithValue("@DateofConfirmation", Employee.DateofConfirmation);
                    cmd.Parameters.AddWithValue("@Gender", Employee.Gender);
                    cmd.Parameters.AddWithValue("@Maritalstatus", Employee.Maritalstatus);
                    cmd.Parameters.AddWithValue("@Qualification", Employee.Qualification);
                    cmd.Parameters.AddWithValue("@Address", Employee.Address);
                    cmd.Parameters.AddWithValue("@Contact", Employee.Contact);
                    cmd.Parameters.AddWithValue("@PAN", Employee.PAN);
                    cmd.Parameters.AddWithValue("@PFAccountNo", Employee.PFAccountNo);
                    cmd.Parameters.AddWithValue("@Aadhar", Employee.Aadhar);
                    cmd.Parameters.AddWithValue("@Passport", Employee.Passport);
                    cmd.Parameters.AddWithValue("@DOB", Employee.DOB);
                    cmd.Parameters.AddWithValue("@Grade", Employee.Grade);
                    cmd.Parameters.AddWithValue("@Grosspay", Employee.Grosspay);
                    cmd.Parameters.AddWithValue("@Age", Employee.Age);
                    cmd.Parameters.AddWithValue("@Email", Employee.Email);
                    cmd.Parameters.AddWithValue("@LName", Employee.LName);
                    cmd.Parameters.AddWithValue("@Alternativeno", Employee.Alternativeno);
                    cmd.Parameters.AddWithValue("@offcEmail", Employee.offcEmail);
                    cmd.Parameters.AddWithValue("@PermanentAddress", Employee.PermanentAddress);
                    cmd.Parameters.AddWithValue("@Department", Employee.Department);
                    cmd.Parameters.AddWithValue("@Empstatus", Employee.Empstatus);
                    cmd.Parameters.AddWithValue("@Location", Employee.Location);
                    cmd.Parameters.AddWithValue("@Reportingmanager", Employee.Reportingmanager);
                    cmd.Parameters.AddWithValue("@Precompanyname", Employee.Precompanyname);
                    cmd.Parameters.AddWithValue("@Predesignation", Employee.Predesignation);
                    cmd.Parameters.AddWithValue("@Preexperience", Employee.Preexperience);
                    cmd.Parameters.AddWithValue("@Bankname", Employee.Bankname);
                    cmd.Parameters.AddWithValue("@Bankacno", Employee.Bankacno);
                    cmd.Parameters.AddWithValue("@Bankbranch", Employee.Bankbranch);
                    cmd.Parameters.AddWithValue("@Bankifsc", Employee.Bankifsc);
                    cmd.Parameters.AddWithValue("@Maritalstatus", Employee.Maritalstatus);
                    cmd.Parameters.AddWithValue("@Bloodgroup", Employee.BloodGroup);
                    cmd.Parameters.AddWithValue("@Availability", Employee.Availability);
                    //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                    cmd.Parameters.AddWithValue("@modified", DateTime.Now);
                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }


            }
            // return RedirectToAction("Index");
            //}
            //return View(Employee);
            return RedirectToAction("Details", "Employee");

        }


        // 3. *************Delete Employee ******************

        // GET: Home/DeleteEmployee/5
        [HandleError]
        public ActionResult DeleteEmployee(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = new Employee();
            string query = "SELECT * FROM EmployeeDetails where Id=" + id;
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
                            employee = new Employee
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"]),
                                Designation = Convert.ToString(sdr["Designation"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]),
                                DateofRelieving = Convert.ToDateTime(sdr["DateofRelieving"]),
                                DateofConfirmation = Convert.ToDateTime(sdr["DateofConfirmation"]),
                                Gender = Convert.ToString(sdr["Gender"]),
                                Qualification = Convert.ToString(sdr["Qualification"]),
                                Address = Convert.ToString(sdr["Address"]),
                                Contact = Convert.ToString(sdr["Contact"]),
                                PAN = Convert.ToString(sdr["PAN"]),
                                PFAccountNo = Convert.ToString(sdr["PFAccountNo"]),
                                Aadhar = Convert.ToString(sdr["Aadhar"]),
                                Passport = Convert.ToString(sdr["Passport"]),
                                DOB = Convert.ToDateTime(sdr["DOB"]),
                                Grade = Convert.ToString(sdr["Grade"]),
                                Grosspay = Convert.ToDecimal(sdr["Grosspay"]),
                                Age = Convert.ToInt32(sdr["Age"]),
                                Email = Convert.ToString(sdr["Email"]),
                                LName = Convert.ToString(sdr["LName"]),
                                Alternativeno = Convert.ToString(sdr["Alternativeno"]),
                                offcEmail = Convert.ToString(sdr["offcEmail"]),
                                PermanentAddress = Convert.ToString(sdr["PermanentAddress"]),
                                Department = Convert.ToString(sdr["Department"]),
                                Empstatus = Convert.ToString(sdr["Empstatus"]),
                                Location = Convert.ToString(sdr["Location"]),
                                Reportingmanager = Convert.ToString(sdr["Reportingmanager"]),
                                Precompanyname = Convert.ToString(sdr["Precompanyname"]),
                                Predesignation = Convert.ToString(sdr["Predesignation"]),
                                Preexperience = Convert.ToString(sdr["Preexperience"]),
                                Bankname = Convert.ToString(sdr["Bankname"]),
                                Bankacno = Convert.ToString(sdr["Bankacno"]),
                                Bankbranch = Convert.ToString(sdr["Bankbranch"]),
                                Bankifsc = Convert.ToString(sdr["Bankifsc"]),
                                Maritalstatus = Convert.ToString(sdr["Maritalstatus"]),
                                BloodGroup = Convert.ToString(sdr["Bloodgroup"]),
                                Availability = Convert.ToString(sdr["Availability"])

                            };
                        }
                    }
                    con.Close();
                }
            }
            return View(employee);
        }

        // POST: Home/DeleteEmployee/5
        [HandleError]
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "Delete FROM EmployeeDetails where Id='" + id + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("Details");
        }

        // 2. *************Create dropdown Details ******************

        [HttpGet]
        [HandleError]
        public ActionResult CreateDropdown()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult CreateDropdown(Empalldropdown e)
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

        // 1. *************show Dropdown list ******************
        // GET: Home
        [HandleError]
        public ActionResult Dropdownsettingslist()
        {
            List<Empalldropdown> Empalldropdownlist = new List<Empalldropdown>();
            string query = "SELECT * FROM Empalldropdown ORDER BY Id";
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
            return View(Empalldropdownlist);
        }

        // GET: Home/DropdownSettingDetails/5
        [HandleError]
        public ActionResult DropdownDetails(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empalldropdown empalldropdown = new Empalldropdown();

            string query = "SELECT * FROM Empalldropdown where Id=" + id;
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
                            empalldropdown = new Empalldropdown
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Type = Convert.ToString(sdr["Type"]),
                                Abbreviation = Convert.ToString(sdr["Abbreviation"]),
                                Description = Convert.ToString(sdr["Description"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (empalldropdown == null)
            {
                return HttpNotFound();
            }
            return View(empalldropdown);
        }

        // 1. *************Get Designation******************
        // GET: Home
        [HandleError]
        public List<Empalldropdown> GetLookup(string type)
        {
            //SELECT distinct      [type]  FROM[Payroll]. [Empalldropdown]

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
                                Type = Convert.ToString(sdr["Type"]),
                                Abbreviation = Convert.ToString(sdr["Abbreviation"]),
                                Description = Convert.ToString(sdr["Description"])
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


        // 3. *************Update Dropdown Detail ******************

        // GET: Home/UpdateDropdown/5
        [HandleError]
        public ActionResult UpdateDropdown(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (id == 0)
            //{
            //    return View();
            //}
            Empalldropdown empalldropdown = new Empalldropdown();
            string query = "SELECT * FROM Empalldropdown where Id=" + id;
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
                            empalldropdown = new Empalldropdown
                            {

                                Id = Convert.ToInt32(sdr["Id"]),
                                Type = Convert.ToString(sdr["Type"]),
                                Abbreviation = Convert.ToString(sdr["Abbreviation"]),
                                Description = Convert.ToString(sdr["Description"]),

                            };
                        }
                    }
                    con.Close();
                }
            }
            if (empalldropdown == null)
            {
                return HttpNotFound();
            }
            return View(empalldropdown);
        }

        // POST: Home/UpdateDropdown/5   
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDropdown([Bind(Include = "Id,type,abbreviation,description")] Empalldropdown Empalldropdown)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Empalldropdown SET type = @type, abbreviation= @abbreviation,description=@description Where Id =@Id";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", Empalldropdown.Id);
                        cmd.Parameters.AddWithValue("@type", Empalldropdown.Type);
                        cmd.Parameters.AddWithValue("@abbreviation", Empalldropdown.Abbreviation);
                        cmd.Parameters.AddWithValue("@description", Empalldropdown.Description);

                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                // return RedirectToAction("Index");
            }
            return View(Empalldropdown);
        }


        // 3. *************Delete Dropdown******************

        // GET: Home/DeleteDropdown/5
        //[HandleError]
        //public ActionResult DeleteDropdown(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Empalldropdown empalldropdown = new Empalldropdown();
        //    string query = "SELECT * FROM Empalldropdown where Id =" + id;
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
        //                    empalldropdown = new Empalldropdown
        //                    {
        //                        Id = Convert.ToInt32(sdr["Id"]),
        //                        Type = Convert.ToString(sdr["Type"]),
        //                        Abbreviation = Convert.ToString(sdr["Abbreviation"]),
        //                        Description = Convert.ToString(sdr["Description"])

        //                    };
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return View(empalldropdown);
        //}

        // POST: Home/DeleteDropdown/5
        [HandleError]
        [HttpGet, ActionName("DeleteDropdown")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteDropdown(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "Delete FROM empalldropdown where Id='" + id + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ViewBag.AlertMsg = "Record Deleted Successfully";
                    }
                }
                return RedirectToAction("Dropdownsettingslist");
            }
            catch
            {
                return View();
            }
        }


        // 3. *************Update Payslip Detail ******************

        // GET: Home/UpdatePayslipDetails/5
        [HandleError]
        public ActionResult UpdatePayslipDetails(int? id, DateTime month)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.Gradelist = GetLookup("Grade");
            ViewBag.PaymentMode = GetLookup("PaymentMode");

            string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            //using (AppEntities1 db = new AppEntities1())
            //{
            //    List<Emp> employees = db.Emps.ToList();

            //    if (employees != null)
            //    {
            //        ViewBag.employees = employees;
            //    }
            //}

            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

            string query = "SELECT em.id, em.EmployeeId, em.name +' '+ em.LName  as Name, em.Grade,em.DOB, em.DateofJoin,em.PFAccountNo, em.Grosspay,e.id as entryid, g.SequenceNumber as LineNumber,  e.MonthYear, isnull(g.Id, 0) as gradeid, " +
         Environment.NewLine + " g.SectionDescription, g.GradeName, " +
         Environment.NewLine + " g.description as Gradeval, " +
         Environment.NewLine + " g.Percentage, g.Description, isnull(e.MonthlyAmount, 0) as MonthlyAmount, " +
         Environment.NewLine + " isnull(e.AnnualAmount, 0) as AnnualAmount, " +
         Environment.NewLine + " isnull(h.LOP, 0) as LOP ,isnull(h.[LeavesTaken],0) as LeavesTaken ,isnull(h.[PaymentMode],0) as PaymentMode " +
         Environment.NewLine + " , emgrd3.description as [Location]" +
         Environment.NewLine + " , emgrd1.description as Department" +
         Environment.NewLine + " , emgrd2.description as Designation " +
         Environment.NewLine + " , emgrd4.description as CompanyName " +
         Environment.NewLine + " FROM EmployeeDetails(nolock)em " +
         Environment.NewLine + " left join PayslipEntry(nolock) e on e.EmployeeId = em.Id " +
         Environment.NewLine + " left join PayslipHeader(nolock) h on e.PayslipHeaderid = h.Id" +
         Environment.NewLine + " left join[Empalldropdown](nolock)emgrd on emgrd.Id = h.Grade" +
         Environment.NewLine + " left join PayslipGrade(nolock) g on emgrd.description = g.GradeName and e.PayslipGradeid = g.Id" +
         Environment.NewLine + " --left join[Empalldropdown](nolock)grd on grd.Id = e.PayslipGradeid" +
         Environment.NewLine + " left join[Empalldropdown](nolock)emgrd1 on emgrd1.Id = em.Department" +
         Environment.NewLine + " left join[Empalldropdown](nolock)emgrd2 on emgrd2.Id = em.Designation" +
         Environment.NewLine + " left join[Empalldropdown](nolock)emgrd3 on emgrd3.Id = em.Location" +
         Environment.NewLine + " left join[Empalldropdown](nolock)emgrd4 on emgrd4.Id = em.CompanyName" +
         Environment.NewLine + " WHERE em.Id =@EmployeeId and month(MonthYear) = month(@month) and  year(MonthYear) =  year(@month) " +
         Environment.NewLine + " ORDER BY g.SequenceNumber";



            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
                            payslipGradeEntry.EmployeeId = Convert.ToString(sdr["EmployeeId"]);
                            payslipGradeEntry.EmpId = Convert.ToInt32(sdr["Id"]);
                            payslipGradeEntry.Id = Convert.ToInt32(sdr["entryid"]);
                            payslipGradeEntry.EmployeeName = sdr["Name"].ToString();
                            payslipGradeEntry.MonthYear = Convert.ToDateTime(sdr["MonthYear"]);
                            payslipGradeHeader.CompanyName = Convert.ToString(sdr["CompanyName"]);
                            payslipGradeEntry.LOP = Convert.ToDecimal(sdr["LOP"]);
                            payslipGradeHeader.LeavesTaken = Convert.ToDecimal(sdr["LeavesTaken"]);
                            payslipGradeHeader.PaymentModeid = Convert.ToInt32(sdr["PaymentMode"]);
                            payslipGradeHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            payslipGradeHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            payslipGradeHeader.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            payslipGradeEntry.EmployeeGrade = Convert.ToString(sdr["GradeName"]);
                            payslipGradeEntry.EmployeeGradeId = Convert.ToString(sdr["Grade"]);
                            payslipGradeEntry.EmployeeGrosspay = Convert.ToDecimal(sdr["Grosspay"]);
                            payslipGradeEntry.Location = Convert.ToString(sdr["Location"]);
                            payslipGradeEntry.Department = Convert.ToString(sdr["Department"]);
                            payslipGradeEntry.Designation = Convert.ToString(sdr["Designation"]);

                            payslipGradeEntry.SectionDescription = Convert.ToString(sdr["SectionDescription"]);
                            payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
                            payslipGradeEntry.Percentage = Convert.ToDecimal(sdr["Percentage"] == DBNull.Value ? 0.00 : sdr["Percentage"]);
                            payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
                            payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
                            payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
                            payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
                        }
                    }
                    con.Close();
                }
                //string query2 = "select Convert(decimal(19,2),isnull(sum(pe.MonthlyAmount),0)) as taxcollected " +
                //                 " from [dbo].[EmployeeDetails] e " +
                //                 " left join[dbo].[PayslipEntry] pe on pe.EmployeeId = e.Id " +
                //                 " left join[dbo].[PayslipGrade] pg on pg.Id = pe.PayslipGradeid " +
                //                 " where pg.Description = 'Income Tax' and e.id = @empid and YEAR(pe.MonthYear) = YEAR(@month)";

                using (SqlCommand cmd = new SqlCommand("sp_Get_TaxCalculated_Amount"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@empid", id);
                    //cmd.Parameters.AddWithValue("@month", DateTime.Now);
                    cmd.Parameters.AddWithValue("@year", month);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    payslipGradeHeader.EmployeeName = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeName;
                    payslipGradeHeader.EmployeeGrade = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrade;
                    payslipGradeHeader.EmployeeGradeID = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGradeId;
                    payslipGradeHeader.EmployeeGrosspay = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrosspay.ToString();
                    payslipGradeHeader.Department = payslipGradeHeader.PayslipGradeEntryList.First().Department;
                    payslipGradeHeader.Designation = payslipGradeHeader.PayslipGradeEntryList.First().Designation;
                    payslipGradeHeader.Location = payslipGradeHeader.PayslipGradeEntryList.First().Location;
                    payslipGradeHeader.MonthYear = payslipGradeHeader.PayslipGradeEntryList.First().MonthYear;
                    payslipGradeHeader.LOP = payslipGradeHeader.PayslipGradeEntryList.First().LOP;
                }
                return View(payslipGradeHeader);
            }
        }

        // POST: Home/UpdatePayslipDetails/5  
        [HandleError]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UpdatePayslipDetails(PayslipGradeHeader payslipGradeHeader, string ExportData)
        {
            //ExportHTML(ExportData);
            //if (ModelState.IsValid)
            //{
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
            string query = "UPDATE PayslipEntry SET MonthlyAmount = @MonthlyAmount, AnnualAmount = @AnnualAmount,LOP = @LOP Where Id =@Id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    // cmd.CommandType = CommandType.StoredProcedure;
                    foreach (PayslipGradeEntry payslipGradeEntry in payslipGradeHeader.PayslipGradeEntryList)
                    {
                        cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@Month", payslipGradeHeader.MonthYear);
                        cmd.Parameters.AddWithValue("@Id", payslipGradeEntry.Id);
                        //cmd.Parameters.AddWithValue("@Name", payslipGradeHeader.EmployeeName);
                        ////cmd.Parameters.AddWithValue("@EmployeeId", payslipGradeHeader.EmployeeId);
                        //cmd.Parameters.AddWithValue("@Grosspay", payslipGradeHeader.EmployeeGrosspay);
                        //cmd.Parameters.AddWithValue("@SectionDescription", payslipGradeEntry.SectionDescription);
                        //cmd.Parameters.AddWithValue("@Description", payslipGradeEntry.Description);
                        //cmd.Parameters.AddWithValue("@Percentage", payslipGradeEntry.Percentage);
                        cmd.Parameters.AddWithValue("@MonthlyAmount", payslipGradeEntry.MonthlyAmount);
                        cmd.Parameters.AddWithValue("@AnnualAmount", payslipGradeEntry.AnnualAmount);
                        cmd.Parameters.AddWithValue("@LOP", payslipGradeHeader.LOP);
                        //cmd.Parameters.AddWithValue("@PayslipGradeid", payslipGradeEntry.PayslipGradeid);
                        // cmd.Parameters.AddWithValue("@Action", "UPDATE");
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return RedirectToAction("Details", "Employee");

                // return View(payslipGradeHeader);
            }

        }
        // 3. *************Delete PayslipDetails ******************



        // GET: Home/DeleteEmployee/5
        [HandleError]
        public ActionResult DeletePayslipDetails(int? id, DateTime month)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();
            string query = "sp_Payslip_Delete";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmpId", id);
                    cmd.Parameters.AddWithValue("@Month", month);
                    con.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return RedirectToAction("Details");
        }

        //// POST: Home/DeleteEmployee/5
        //[HttpPost, ActionName("DeletePayslipDetails")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletePayslipDetails(int id)
        //{
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        string query = "Delete FROM PayslipEntry where Id='" + id + "'";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //    return RedirectToAction("Details");
        //}
        [HandleError]
        public ActionResult Logout()
        {

            //Session["Email"] = "";
            //Session["Id"] = "";
            //Session["AccessType"] = "";
            //Session.Abandon();

            //disable browsers back buttons.

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));

            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "UserLogin");

        }
        public string SaveLogout(string logintime, string logouttime, string comment, string EmployeeId, string workingstatus)
        {
            if (EmployeeId == null)
            {
                return "";
            }
            //update working status for on leave employee
            //string qry = "SELECT d.Name,cast(l.StartDate as date) as StartDate,cast(l.EndDate as date) as EndDate,l.ApproveAction" +
            //                Environment.NewLine + " FROM EmployeeLeaves l join EmployeeDetails d on l.EmployeeId = d.Id" +
            //                Environment.NewLine + " WHERE(StartDate = cast(GETDATE() as date) or EndDate = cast(GETDATE() as date)) and l.ApproveAction = 'Approved'";

            //using (SqlConnection con1 = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd1 = new SqlCommand(qry))
            //    {
            //        cmd1.Connection = con1;
            //        con1.Open();
            //        using (SqlDataReader sdr1 = cmd1.ExecuteReader())
            //        {

            //            while (sdr1.Read())
            //            {
            //                if (Convert.ToString(sdr1["ApproveAction"]) == "Approved")
            //                {
            //                    workingstatus = "Leave";
            //                }
            //            }
            //            con1.Close();
            //        }
            //    }
            //}
            //Attendance Create or Update
            string query = "sp_Attendance_createupdate";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    cmd.Parameters.AddWithValue("@LogIn", logintime == "" ? DateTime.Now : Convert.ToDateTime(logintime));
                    cmd.Parameters.AddWithValue("@LogOut", logouttime == "" ? Convert.ToDateTime("1900-01-01 00:00:00.000") : Convert.ToDateTime(logouttime));
                    cmd.Parameters.AddWithValue("@Comments", comment);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToInt32(Session["id"]));
                    cmd.Parameters.AddWithValue("@WorkingStatus", workingstatus == null ? "" : workingstatus);
                    con.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return "success";
        }

        //public void ExportHTML(string ExportData)
        //{
        //    //HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
        //    //HtmlNode.ElementsFlags["input"] = HtmlElementFlag.Closed;
        //    //HtmlDocument doc = new HtmlDocument();
        //    //doc.OptionFixNestedTags = true;
        //    //doc.LoadHtml(ExportData);
        //    //ExportData = doc.DocumentNode.OuterHtml;
        //    //string HTMLContent = ExportData;
        //    //Response.Clear();
        //    //Response.ContentType = "application/pdf";
        //    //Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
        //    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    //Response.BinaryWrite(GetPDF(HTMLContent));
        //    //Response.End();
        //    using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailFrom"], "hemalatha@zuddhisystems.com"))
        //    {
        //        mm.Subject = "Payslip";
        //        mm.Body = "Please Find the attachement for this month payslip";
        //        mm.Attachments.Add(new Attachment(new MemoryStream(Convert.ToInt32(ExportData)), "PDFfile.pdf"));
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPassword"]);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
        //        smtp.Send(mm);
        //    }
        //}
        //public byte[] GetPDF(string pHTML)
        //{
        //    byte[] bPDF = null;
        //    MemoryStream ms = new MemoryStream();
        //    StringReader txtReader = new StringReader(pHTML);
        //    Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //    PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);
        //    doc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(oPdfWriter, doc, txtReader);
        //    doc.Close();
        //    bPDF = ms.ToArray();

        //    using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailFrom"], "hemalatha@zuddhisystems.com"))
        //    {
        //        mm.Subject = "Payslip";
        //        mm.Body = "Please Find the attachement for this month payslip";
        //        mm.Attachments.Add(new Attachment(new MemoryStream(bPDF), "PDFfile.pdf"));
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPassword"]);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
        //        smtp.Send(mm);
        //    }
        //    return bPDF;
        //}

        //Leave Balance Report List
        [HandleError]
        [HttpGet]
        public ActionResult AttendanceReport(int? id, DateTime? month)
        {
            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.Where(s => s.Availability == "Active").ToList();
                List<Setting> Settings = db.Settings.ToList();
                if (employees != null)
                {
                    ViewBag.employees = employees;
                }
            }
            if (id == null || id == 0)
            {
                id = -2;
            }

            if (month == null && month != DateTime.Now.AddMonths(1))
            {
                month = DateTime.Now;
            }

            return View(GetAttendanceReportList(id, month));
        }

        [HandleError]
        public AttendanceReportHeader GetAttendanceReportList(int? id, DateTime? month)
        {

            AttendanceReportHeader attendanceReportHeaderList = new AttendanceReportHeader();
            //attendanceReportHeaderList.EmpName = id;
            attendanceReportHeaderList.MonthYear = Convert.ToDateTime(month);

            string attendancereport = "sp_attendance_Report";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(attendancereport))
                {

                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.Parameters.AddWithValue("@monthYear", month);
                    con2.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            attendanceReportHeaderList.AttendanceReportList.Add(new AttendanceReport
                            {
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                EmployeeName = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                shiftDate = Convert.ToString(sdr["shiftDate"] == DBNull.Value ? DateTime.Now : sdr["shiftDate"]),
                                LogInTime = Convert.ToString(sdr["LogInTime"] == DBNull.Value ? "" : sdr["LogInTime"]),
                                LogOutTime = Convert.ToString(sdr["LogOutTime"] == DBNull.Value ? "" : sdr["LogOutTime"]),
                                TotalTime = Convert.ToString(sdr["Total_Time"] == DBNull.Value ? "" : sdr["Total_Time"]),
                                WorkStatus = Convert.ToString(sdr["WorkStatus"] == DBNull.Value ? "" : sdr["WorkStatus"])

                            });
                        }

                        con2.Close();
                    }
                }
                if (id != -2)
                {
                    if (attendanceReportHeaderList.AttendanceReportList.Count > 0)
                    {
                        attendanceReportHeaderList.EmpName = attendanceReportHeaderList.AttendanceReportList[0].EmployeeName;
                    }
                    
                }
                ViewBag.leavedetails = attendanceReportHeaderList.AttendanceReportList;
                return attendanceReportHeaderList;
            }
        }


        //For Export Monthly Attendance Report
        public ActionResult MonthlyAttendanceReport(int? id, DateTime? month)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendanceReport oMonthlyAttendanceReport = new MonthlyAttendanceReport();
            oMonthlyAttendanceReport.MonthYear = Convert.ToDateTime(month);
            string attendanceReport = "sp_attendance_Monthly_Report";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(attendanceReport))
                {

                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.Parameters.AddWithValue("@monthYear", month);

                    con2.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            oReportRow = new ReportRow();
                            for (int i = 0; i < sdr.FieldCount; i++)
                            {
                                ReportField oReportField = new ReportField();
                                oReportField.FieldName = Convert.ToString(sdr.GetName(i));
                                oReportField.FieldValue = Convert.ToString(sdr.GetValue(i));
                                oReportRow.ReportFieldList.Add(oReportField);
                            }
                            oMonthlyAttendanceReport.ReportRowList.Add(oReportRow);
                        }
                    }
                    con2.Close();


                }
            }
            //ViewBag.payslips = payslipGradeHeaderList;

            return View(oMonthlyAttendanceReport);
        }
    }
}

