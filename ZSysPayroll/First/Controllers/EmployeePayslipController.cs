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

namespace First.Controllers
{
    public class EmployeePayslipController : Controller
    {
        //AppEntities ctx;
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
        // GET: Enrollment
        public string value = "";
     

        // 2. *************ADD NEW Employee Details ******************
        [HandleError]
        [HttpPost]
        public ActionResult Index(EmployeePayslip e)
        {
            if (Request.HttpMethod == "POST")
            {
                //Employee er = new Employee();

                string Department = Request.Form["Department"].ToString();
                string Designation = Request.Form["Designation"].ToString();
                string BloodGroup = Request.Form["Bloodgroup"].ToString();
                string Qualification = Request.Form["Qualification"].ToString();
                string Grade = Request.Form["Grade"].ToString();

                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("SP_Employee", con))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", e.Id);
                        cmd.Parameters.AddWithValue("@Name", e.Name);
                        cmd.Parameters.AddWithValue("@Designation", e.Designation);
                        cmd.Parameters.AddWithValue("@EmployeeId", e.EmployeeId);

                        if (e.DateofJoin.ToString() != "01-01-0001 00:00:00")
                            cmd.Parameters.AddWithValue("@DateofJoin", e.DateofJoin);
                        else
                        {
                            DateTime curDt = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DateofJoin", curDt);
                        }
                        cmd.Parameters.AddWithValue("@Gender", e.Gender);
                        cmd.Parameters.AddWithValue("@Qualification", e.Qualification);
                        cmd.Parameters.AddWithValue("@Address", e.Address);
                        cmd.Parameters.AddWithValue("@Contact", e.Contact);
                        cmd.Parameters.AddWithValue("@PAN", e.PAN);
                        cmd.Parameters.AddWithValue("@PFAccountNo", e.PFAccountNo);
                        cmd.Parameters.AddWithValue("@Aadhar", e.Aadhar);
                        cmd.Parameters.AddWithValue("@Passport", e.Passport);

                        if (e.DOB.ToString() != "01-01-0001 00:00:00")
                            cmd.Parameters.AddWithValue("@DOB", e.DOB);
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
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            //return View();

            return RedirectToAction("GeneratePayslip", "Employee");
        }

        // 1. *************show the list of Employee ******************
        // GET: Home

        [HandleError]
        [HttpGet]
        public ActionResult GeneratePayslip()
        {
            List<EmployeePayslip> Employees = new List<EmployeePayslip>();
            string query = "";
            string EmpID = "";
            EmpID = Convert.ToString(Session["Id"]);
            
                query = "SELECT * FROM EmployeeDetails WHERE Availability = 'Active' ORDER BY EmployeeId";
           
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
                            Employees.Add(new EmployeePayslip
                            {
                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                Designation = Convert.ToString(sdr["Designation"] == DBNull.Value ? "" : sdr["Designation"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]),
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
                                Availability = Convert.ToString(sdr["Availability"] == DBNull.Value ? "" : sdr["Availability"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            
            if (Employees.Count == 0)
            {
                Employees.Add(new EmployeePayslip());
            }
            return View(Employees);
        }

        // 1. *************submit post event ******************
        // GET: Home

        [HandleError]
        [HttpPost]
        public ActionResult GeneratePayslip(List<EmployeePayslip> EmployeesList)
        {
            List<EmployeePayslip> Employees = new List<EmployeePayslip>();
            string query = "";            

            query = "SELECT * FROM EmployeeLeaveDetails WHERE Availability = 'Active' ORDER BY EmployeeId";

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
                            Employees.Add(new EmployeePayslip
                            {
                               
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                MonYear = Convert.ToString(sdr["MonYear"] == DBNull.Value ? "" : sdr["MonYear"]),
                                LeavesTaken = Convert.ToDecimal(sdr["LeavesTaken"] == DBNull.Value ? 0 : sdr["LeavesTaken"]),
                                LeavesAccumulated = Convert.ToDecimal(sdr["LeavesAccumulated"] == DBNull.Value ? 0 : sdr["LeavesAccumulated"]),
                                LeavesAvailable = Convert.ToDecimal(sdr["LeavesAvailable"] == DBNull.Value ? 0 : sdr["LeavesAvailable"]),
                                LOP = Convert.ToDecimal(sdr["LOP"] == DBNull.Value ? 0 : sdr["LOP"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (Employees.Count == 0)
            {
                Employees.Add(new EmployeePayslip());
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


            string payslip = "select e.EmployeeId, e.Id, e.Name, p.MonthYear ,h.GrossSalary,h.TotalDeductions,h.NetSalary"+
                Environment.NewLine + ",(select sum(p1.MonthlyAmount) from PayslipEntry p1 LEFT JOIN PayslipGrade g1 on g1.Id = p1.PayslipGradeid where p1.EmployeeId = e.Id and g1.Description = 'IT'" +
                Environment.NewLine + "and p1.MonthYear = p.MonthYear) as ITpaid" +
                Environment.NewLine + "from EmployeeDetails e" +
                Environment.NewLine + "LEFT JOIN PayslipEntry p on p.EmployeeId = e.Id" +
                Environment.NewLine + "LEFT JOIN PayslipGrade g on g.Id = p.PayslipGradeid" +
                Environment.NewLine + "left join[dbo].[PayslipHeader] h on h.Id = p.PayslipHeaderid" +
                Environment.NewLine + "WHERE e.Id = " + id+
                Environment.NewLine + "GROUP BY e.Id,e.Name, p.MonthYear,e.EmployeeId,h.GrossSalary,h.TotalDeductions,h.NetSalary"; 

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {
                    cmd2.Connection = con2;
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
                                    EmpId = Convert.ToInt32(id),                                    
                                    EmployeeName = Convert.ToString(sdr["Name"]),
                                    MonthYear = Convert.ToDateTime(sdr["MonthYear"]),
                                    GrossSalary=Convert.ToDecimal(sdr["GrossSalary"]),
                                    TotalDeductions=Convert.ToDecimal(sdr["TotalDeductions"]),
                                    NetSalary=Convert.ToDecimal(sdr["NetSalary"]),
                                    taxcollected = Convert.ToDecimal(sdr["ITpaid"])

                                });
                            }
                        }
                    }
                    con2.Close();
                }
            }
            ViewBag.payslips = payslipGradeHeaderList;

            List<Form80CHeader> Form80CHeaderlist = new List<Form80CHeader>();


            string form = "select max (e.EmployeeId) as EmployeeId, max (e.Id) as Id, max (e.Name) as Name, max(isnull(y.CreatedDate,getdate())) as CreatedDate from EmployeeDetails e" +
            Environment.NewLine + "join [TaxDeclFormEntry] y on y.EmployeeId = e.Id" +
            Environment.NewLine + "left join [TaxDeclFormSettings] s on s.Id = y.[TaxDeclFormid]" +
            Environment.NewLine + "where e.Id =" + id + " " +
            Environment.NewLine + "group by year(isnull(y.CreatedDate,getdate()))";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(form))
                {
                    cmd2.Connection = con2;
                    con2.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80CHeaderlist.Add(new Form80CHeader
                            {
                                Employeeid = Convert.ToString(sdr["EmployeeId"]),
                                Empid = Convert.ToInt32(sdr["Id"]),
                                EmployeeName = Convert.ToString(sdr["Name"]),
                                CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                            });
                        }
                    }
                    con2.Close();
                }
            }
            ViewBag.deductions = Form80CHeaderlist;



            Employee emp = new Employee();
            string query = "SELECT e.Id," +
            Environment.NewLine+ "[Name],"+
            Environment.NewLine+ "[Designation]," +
            Environment.NewLine+ "desg.description as DesignationName," +
            Environment.NewLine + "[EmployeeId]," +
            Environment.NewLine+ "[DateofJoin]," +
            Environment.NewLine + " isnull([Gender],'') as [Gender]," +
            Environment.NewLine+ "[Qualification]," +
            Environment.NewLine+ "edu.description as QualificationName," +
            Environment.NewLine+ "[Address]," +
            Environment.NewLine+ "[Contact], " +
            Environment.NewLine+ "[PAN], " +
            Environment.NewLine + "[PFAccountNo], " +
            Environment.NewLine+ "[Aadhar]," +
            Environment.NewLine+ "[Passport]," +
            Environment.NewLine+ "[DOB]," +
            Environment.NewLine+ "grad.[description] as Grade, " +
            Environment.NewLine + "[Grosspay], " +
            Environment.NewLine+ "[Age]," +
            Environment.NewLine+ " isnull([PId],'') as [PId], " +
            Environment.NewLine+ "[Created]," +
            Environment.NewLine+ "[modified], " +
            Environment.NewLine+ " isnull([Email],'') as [Email], " +
            Environment.NewLine+ " isnull([LName],'') as [LName], " +
            Environment.NewLine+ " isnull([Alternativeno],'') as [Alternativeno], " +
            Environment.NewLine+ " isnull([offcEmail],'') as [offcEmail], " +
            Environment.NewLine+ " isnull([PermanentAddress],'') as [PermanentAddress], " +
            Environment.NewLine+ "[Department], " +
            Environment.NewLine+ "dept.description as DepartmentName," +
            Environment.NewLine+ " isnull([Empstatus],'') as [Empstatus]," +
            Environment.NewLine+ " isnull([Reportingmanager],'') as [Reportingmanager], " +
            Environment.NewLine+ " isnull([Precompanyname],'') as [Precompanyname], " +
            Environment.NewLine+ " isnull([Predesignation],'') as [Predesignation], " +
            Environment.NewLine+ " isnull([Preexperience],'') as [Preexperience]," +
            Environment.NewLine+ " isnull([Bankname],'') as [Bankname], " +
            Environment.NewLine+ " isnull([Bankacno],'') as [Bankacno], " +
            Environment.NewLine+ " isnull([Bankbranch],'') as [Bankbranch], " +
            Environment.NewLine+ " isnull([Bankifsc],'') as [Bankifsc]," +
            Environment.NewLine+ " isnull([Maritalstatus],'') as [Maritalstatus]," +
            Environment.NewLine + " isnull([Availability],'') as [Availability], " +
            Environment.NewLine+ "[Bloodgroup]," +
            Environment.NewLine+ "blood.description as BloodgroupName" +            
            Environment.NewLine+ " FROM EmployeeDetails e" +
            Environment.NewLine+ " left join [Empalldropdown] desg on e.Designation = desg.Id" +
            Environment.NewLine+ " left join [Empalldropdown] blood on e.Bloodgroup = blood.Id" +
            Environment.NewLine+ " left join [Empalldropdown] edu on e.Qualification = edu.Id" +
            Environment.NewLine+ " left join [Empalldropdown] dept on e.Department = dept.Id" +
            Environment.NewLine +" left join [Empalldropdown] grad on e.Grade = grad.Id" +
            Environment.NewLine + " where e.Id = @id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            emp = new Employee
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"]),
                                Designation = Convert.ToString(sdr["DesignationName"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]),
                                Gender = Convert.ToString(sdr["Gender"]),
                                Qualification = Convert.ToString(sdr["QualificationName"]),
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
                                Department = Convert.ToString(sdr["DepartmentName"]),
                                Empstatus = Convert.ToString(sdr["Empstatus"]),
                                Reportingmanager = Convert.ToString(sdr["Reportingmanager"]),
                                Precompanyname = Convert.ToString(sdr["Precompanyname"]),
                                Predesignation = Convert.ToString(sdr["Predesignation"]),
                                Preexperience = Convert.ToString(sdr["Preexperience"]),
                                Bankname = Convert.ToString(sdr["Bankname"]),
                                Bankacno = Convert.ToString(sdr["Bankacno"]),
                                Bankbranch = Convert.ToString(sdr["Bankbranch"]),
                                Bankifsc = Convert.ToString(sdr["Bankifsc"]),
                                Maritalstatus = Convert.ToString(sdr["Maritalstatus"]),
                                BloodGroup = Convert.ToString(sdr["BloodgroupName"]),
                                Availability=Convert.ToString(sdr["Availability"])
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
        public ActionResult Index(int? id)
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
            EmployeePayslip emp = new EmployeePayslip();
            if (id == 0)
            {
                //Employee emp = new Employee();
                AppEntities1 db = new AppEntities1();
                //sort the employee and get the last insert employee.
                var lastemployee = db.Emps.OrderByDescending(c => c.EmployeeId).FirstOrDefault();
                if (lastemployee == null)
                {
                    emp.EmployeeId = "ZS001";
                }
                else
                {
                    //using string substring method to get the number of the last inserted employee's EmployeeID 
                    emp.EmployeeId = "ZS" + (Convert.ToInt32(lastemployee.EmployeeId.Substring(2, lastemployee.EmployeeId.Length - 2)) + 1).ToString("D3");
                }

                //ViewBag.Designationlist = GetLookup("Designation");
                //ViewBag.Bloodgrouplist = GetLookup("Bloodgroup");
                //ViewBag.Departmentlist = GetLookup("Department");
                //ViewBag.Qualificationlist = GetLookup("Qualification");
                //ViewBag.Gradelist = GetLookup("Grade");
                return View(emp);
            }
            
            string query = "SELECT * FROM EmployeeDetails where Id=" + id;
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
                            emp = new EmployeePayslip
                            {

                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "" : sdr["Name"]),
                                Designation = Convert.ToString(sdr["Designation"] == DBNull.Value ? "" : sdr["Designation"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"] == DBNull.Value ? "" : sdr["EmployeeId"]),
                                DateofJoin = Convert.ToDateTime(sdr["DateofJoin"] == DBNull.Value ? DateTime.Now : sdr["DateofJoin"]),
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
            //if (Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
            //{
            //    
            //}
            //else
            //{
            //    return RedirectToAction("EmployeeDetails","Employee");
            //}

        }

        // POST: Home/UpdateEmployee/5  
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee([Bind(Include = "Id,Name,Designation,EmployeeId,DateofJoin,Gender,Qualification,Address,Contact,PAN,PFAccountNo,Aadhar,Passport,DOB,Grade,Grosspay,Age,Email,LName,Alternativeno,offcEmail,PermanentAddress,Department,Empstatus,Reportingmanager,Precompanyname,Predesignation,Preexperience,Bankname,Bankacno,Bankbranch,Bankifsc,Maritalstatus,Bloodgroup,Availability,modified")] Employee Employee)
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
                                Availability=Convert.ToString(sdr["Availability"])

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

            string query = "SELECT em.id, em.EmployeeId, em.Name, em.Grade, em.Grosspay,e.id as entryid, g.LineNumber,  e.MonthYear, isnull(g.Id, 0) as gradeid, " +
                            " g.SectionDescription, g.GradeName, " +
                            " grd.description as Gradeval, " +
                            " g.Percentage, g.Description, isnull(e.MonthlyAmount, 0) as MonthlyAmount, " +
                            " isnull(e.AnnualAmount, 0) as AnnualAmount, " +
                            " isnull(e.LOP, 0) as LOP " +
                            " FROM EmployeeDetails(nolock)em " +
                            " left join [Empalldropdown] emgrd on emgrd.Id = em.Grade" +
                            " left join PayslipGrade(nolock) g on emgrd.description = g.GradeName" +
                            " left join PayslipEntry(nolock) e on e.EmployeeId = em.Id and e.PayslipGradeid = g.Id" +
                            " left join [Empalldropdown] grd on grd.Id = e.PayslipGradeid" +
                            " WHERE em.Id =@EmployeeId and month(MonthYear) = month(@month) and  year(MonthYear) =  year(@month) " +
                            " ORDER BY g.LineNumber";

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
                            payslipGradeEntry.LOP = Convert.ToDecimal(sdr["LOP"]);
                            payslipGradeEntry.EmployeeGrade = Convert.ToString(sdr["GradeName"]);
                            payslipGradeEntry.EmployeeGrosspay = Convert.ToDecimal(sdr["Grosspay"]);
                            payslipGradeEntry.SectionDescription = Convert.ToString(sdr["SectionDescription"]);
                            payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
                            payslipGradeEntry.Percentage = Convert.ToDecimal(sdr["Percentage"]);
                            payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
                            payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
                            payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
                            payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
                        }
                    }
                    con.Close();
                }
                string query2 = "select isnull(sum(pe.MonthlyAmount),0) as taxcollected " +
                                 " from [dbo].[Emp] e " +
                                 " left join[dbo].[PayslipEntry] pe on pe.EmployeeId = e.Id " +
                                 " left join[dbo].[PayslipGrade] pg on pg.Id = pe.PayslipGradeid " +
                                 " where pg.Description = 'IT' and e.id = @empid and YEAR(pe.MonthYear) = YEAR(@month)";
                using (SqlCommand cmd = new SqlCommand(query2))
                {
                    cmd.Connection = con;
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
                    payslipGradeHeader.EmployeeName = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeName;
                    payslipGradeHeader.EmployeeGrade = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrade;
                    payslipGradeHeader.EmployeeGrosspay = payslipGradeHeader.PayslipGradeEntryList.First().EmployeeGrosspay.ToString();
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
        public ActionResult UpdatePayslipDetails( PayslipGradeHeader payslipGradeHeader)
        {
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
                    cmd.Parameters.AddWithValue("@NetSalary", payslipGradeHeader.NetSalary);
                    cmd.Parameters.AddWithValue("@GrossSalary", payslipGradeHeader.GrossSalary);
                    cmd.Parameters.AddWithValue("@TotalDeductions", payslipGradeHeader.TotalDeductions);
                    cmd.Parameters.AddWithValue("@isSalaryStructure", payslipGradeHeader.isSalaryStructure);
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
            return RedirectToAction("Details","Employee");
            
           // return View(payslipGradeHeader);
        }      

    }

        //// 3. *************Delete PayslipDetails ******************

        //// GET: Home/DeleteEmployee/5
        //public ActionResult DeletePayslipDetails(int? id,DateTime month)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
        //    payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

        //    string query = "SELECT em.id,em.Name,em.Grade,em.Grosspay, g.LineNumber,e.id as entryid, g.Id as gradeid, g.SectionDescription, g.GradeName, g.Percentage, g.Description,isnull(e.MonthlyAmount, 0) as MonthlyAmount, isnull(e.AnnualAmount, 0) as AnnualAmount "
        //                    + " from emp(nolock) em "
        //                    + "left "
        //                    + "join PayslipGrade(nolock) g on em.Grade = g.GradeName "
        //                    + "left "
        //                    + "join PayslipEntry(nolock) e on e.EmployeeId = em.Id and e.PayslipGradeid = g.Id "
        //                    + " WHERE em.Id =@EmployeeId and month(MonthYear) = month(@month) and  year(MonthYear) =  year(@month) "
        //                     + " ORDER BY g.LineNumber";


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
        //                    PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
        //                    payslipGradeEntry.EmployeeId = Convert.ToInt32(sdr["Id"]);
        //                    payslipGradeEntry.Id = Convert.ToInt32(sdr["entryid"]);
        //                    payslipGradeEntry.EmployeeName = Convert.ToString(sdr["Name"]);
        //                    payslipGradeEntry.EmployeeGrade = Convert.ToString(sdr["Grade"]);
        //                    payslipGradeEntry.EmployeeGrosspay = Convert.ToDecimal(sdr["Grosspay"]);
        //                    payslipGradeEntry.SectionDescription = Convert.ToString(sdr["SectionDescription"]);
        //                    payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
        //                    payslipGradeEntry.Percentage = Convert.ToInt32(sdr["Percentage"]);
        //                    payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
        //                    payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
        //                    payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
        //                    payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return View(payslipGradeHeader);
        //}

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

    }
}
