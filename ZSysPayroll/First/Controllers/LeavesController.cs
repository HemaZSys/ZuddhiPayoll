using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using First.Models;
using System.Net;
using System.Net.Mail;

///using First.Models;
///using System.Security.Cryptography;


namespace First.Controllers
{
    public class LeavesController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;

        [HandleError]
        [HttpGet]
        public ActionResult LeaveApproval()
        {
            //ViewBag.Leaveslist = GetLookup("LeaveType");
            return View();
        }
        [HandleError]
        [HttpGet]
        public ActionResult LeaveList(int id, string mode)
        {
            ViewBag.LeaveList = GetLeaveList(id, mode);
            return View(ViewBag.LeaveList);
        }

        [HandleError]
        public LeaveDetailsHeader GetLeaveList(int id, string mode)
        {
            //string EmpID = Convert.ToString(Session["Id"]);            
            var spName = "";
            LeaveDetailsHeader leaveDetailsHeader = new LeaveDetailsHeader();
            List<EmployeeLeave> oListEmployeeLeave = new List<EmployeeLeave>();
            if (mode == "ApplyList")
            {
                spName = "sp_EditLeave_List";
            }
            else if (mode == "ApproveList")
            {
                spName = "sp_ApproveLeave_List";
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(spName))
                {
                    cmd.Connection = con;
                    con.Open();                    
                    cmd.Parameters.AddWithValue("@employeeid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            leaveDetailsHeader.EmployeeLeaveList.Add(new EmployeeLeave
                            {

                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : sdr["Id"]),
                                EmpId = Convert.ToInt32(sdr["EmployeeId"] == DBNull.Value ? 0 : sdr["EmployeeId"]),
                                EmpCode = Convert.ToString(sdr["EmpCode"] == DBNull.Value ? 0 : sdr["EmpCode"]),
                                sessionId = Convert.ToInt32(sdr["sessionid"] == DBNull.Value ? 0 : sdr["sessionid"]),
                                LeaveType = Convert.ToInt32(sdr["LeaveType"] == DBNull.Value ? 0 : sdr["LeaveType"]),
                                EmpName = Convert.ToString(sdr["EmployeeName"] == DBNull.Value ? "" : sdr["EmployeeName"]),
                                startDate = Convert.ToDateTime(sdr["StartDate"] == DBNull.Value ? DateTime.Now : sdr["StartDate"]),
                                EndDate = Convert.ToDateTime(sdr["EndDate"] == DBNull.Value ? DateTime.Now : sdr["EndDate"]),
                                NoOfDays = Convert.ToDecimal(sdr["NoOfDays"] == DBNull.Value ? 0 : sdr["NoOfDays"]),
                                LeaveStatus = Convert.ToString(sdr["LeaveStatus"] == DBNull.Value ? "" : sdr["LeaveStatus"]),
                                LeaveReason = Convert.ToString(sdr["LeaveReason"] == DBNull.Value ? "" : sdr["LeaveReason"]),
                                Reportingmanager = Convert.ToString(sdr["ReportingPerson"] == DBNull.Value ? "" : sdr["ReportingPerson"]),
                                LeaveBalance = Convert.ToInt32(sdr["LeaveBalance"] == DBNull.Value ? 0 : sdr["LeaveBalance"]),
                                ApproveAction = Convert.ToString(sdr["ApproveAction"] == DBNull.Value ? "" : sdr["ApproveAction"]),
                                ReportingmanagerId = Convert.ToInt32(sdr["ReportingmanagerId"] == DBNull.Value ? 0 : sdr["ReportingmanagerId"]),
                                CasualLeaveBalance = Convert.ToInt32(sdr["CasualLeaveBalance"] == DBNull.Value ? 0 : sdr["CasualLeaveBalance"]),
                                SickLeaveBalance = Convert.ToInt32(sdr["SickLeaveBalance"] == DBNull.Value ? 0 : sdr["SickLeaveBalance"])
                            });
                        }
                        con.Close();
                    }
                }
                ViewBag.leavelistdetails = leaveDetailsHeader.EmployeeLeaveList;
                return leaveDetailsHeader;
            }

        }

        [HandleError]
        [HttpGet]
        public ActionResult LeaveSetting()
        {
            return View();
        }
        [HandleError]
        [HttpGet]
        public ActionResult ApplyLeave(int id, int lid, string mode)
        {
            RefreshLeaveReport(id);
            EmployeeLeave oEmployeeLeave = new EmployeeLeave();            
            var spName = "";
            string EmpID = Convert.ToString(Session["Id"]);
            string AccessType = Convert.ToString(Session["AccessType"]);
            if (mode == "Apply")
            {
                spName = "sp_EmployeeLeavesApply_Get";
            }
            else if (mode == "Approve")
            {
                spName = "sp_EmployeeLeavesApprove_Get";
            }

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(spName))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Connection = con2;                   
                    if (mode == "Apply")
                    {
                        cmd2.Parameters.AddWithValue("@id", EmpID);
                        cmd2.Parameters.AddWithValue("@rid", id);
                        cmd2.Parameters.AddWithValue("@lid", lid);
                    }
                    else if (mode == "Approve")
                    {
                        if (AccessType != null && AccessType != "User")
                        {
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.Parameters.AddWithValue("@rid", -2);
                            cmd2.Parameters.AddWithValue("@lid", lid);
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.Parameters.AddWithValue("@rid", EmpID);
                            cmd2.Parameters.AddWithValue("@lid", lid);
                        }
                        
                    }
                                      
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            oEmployeeLeave.Id = Convert.ToInt32(sdr["id"] == DBNull.Value ? 0 : sdr["id"]);
                            oEmployeeLeave.LeaveId = Convert.ToInt32(sdr["LeaveId"] == DBNull.Value ? 0 : sdr["LeaveId"]);
                            oEmployeeLeave.EmpId = Convert.ToInt32(sdr["EmployeeId"] == DBNull.Value ? 0 : sdr["EmployeeId"]);
                            oEmployeeLeave.sessionId = Convert.ToInt32(sdr["sessionid"] == DBNull.Value ? EmpID : sdr["sessionid"]);
                            oEmployeeLeave.EmpName = Convert.ToString(sdr["name"] == DBNull.Value ? "" : sdr["name"]);
                            oEmployeeLeave.UserEmailId = Convert.ToString(sdr["UserEmailId"] == DBNull.Value ? "" : sdr["UserEmailId"]);
                            oEmployeeLeave.ReportingMgrEmailID = Convert.ToString(sdr["ReportingMgrEmailID"] == DBNull.Value ? "" : sdr["ReportingMgrEmailID"]);
                            oEmployeeLeave.LeaveType = Convert.ToInt32(sdr["LeaveType"] == DBNull.Value ? 0 : sdr["LeaveType"]);
                            oEmployeeLeave.startDate = Convert.ToDateTime(sdr["startDate"] == DBNull.Value ? DateTime.Now : sdr["startDate"]);
                            oEmployeeLeave.EndDate = Convert.ToDateTime(sdr["EndDate"] == DBNull.Value ? DateTime.Now : sdr["EndDate"]);
                            oEmployeeLeave.NoOfDays = Convert.ToDecimal(sdr["NoOfDays"] == DBNull.Value ? 0 : sdr["NoOfDays"]);
                            oEmployeeLeave.LeaveStatus = Convert.ToString(sdr["LeaveStatus"] == DBNull.Value ? "" : sdr["LeaveStatus"]);
                            oEmployeeLeave.LeaveReason = Convert.ToString(sdr["LeaveReason"] == DBNull.Value ? "" : sdr["LeaveReason"]);
                            oEmployeeLeave.LeaveBalance = Convert.ToInt32(sdr["LeaveBalance"] == DBNull.Value ? 0 : sdr["LeaveBalance"]);
                            oEmployeeLeave.Reportingmanager = Convert.ToString(sdr["ReportingmanagerName"] == DBNull.Value ? "" : sdr["ReportingmanagerName"]);
                            oEmployeeLeave.ApproveAction = Convert.ToString(sdr["ApproveAction"] == DBNull.Value ? "Pending" : sdr["ApproveAction"]);
                            oEmployeeLeave.ApproveActionStatus = Convert.ToString(sdr["ApproveAction"] == DBNull.Value ? "Pending" : sdr["ApproveAction"]);
                            oEmployeeLeave.ReportingmanagerId = Convert.ToInt32(sdr["Reportingmanager"] == DBNull.Value ? 0 : sdr["Reportingmanager"]);
                            oEmployeeLeave.CasualLeaveBalance = Convert.ToInt32(sdr["CasualLeaveBalance"] == DBNull.Value ? 0 : sdr["CasualLeaveBalance"]);
                            oEmployeeLeave.SickLeaveBalance = Convert.ToInt32(sdr["SickLeaveBalance"] == DBNull.Value ? 0 : sdr["SickLeaveBalance"]);
                        }
                    }
                    con2.Close();
                }
            }
           
            if (AccessType != null && AccessType != "User")
            {
                EmpID = Convert.ToString(-2);
            }

            if (string.IsNullOrEmpty(oEmployeeLeave.ApproveAction))
            {
                oEmployeeLeave.ApproveAction = "Pending";
            }
            if (oEmployeeLeave.ApproveAction == "Approved" || oEmployeeLeave.ApproveAction == "Rejected")
            {
                return RedirectToAction("LeaveList", "Leaves", new {id= EmpID, mode = "ApplyList" });
            }
            ViewBag.Leaveslist = GetLookup("LeaveType");
            ViewBag.AllEmployeeList = GetAllEmployeeList();
            return View(oEmployeeLeave);
        }

        // GET: Leaves
        [HandleError]
        [HttpPost]
        public ActionResult ApplyLeave(EmployeeLeave oEmployeeLeave)
        {
            SendEmailNotification(oEmployeeLeave.UserEmailId, oEmployeeLeave.ReportingMgrEmailID);
            RefreshLeaveReport(oEmployeeLeave.EmpId);
            string EmpID = Convert.ToString(Session["Id"]);
            string AccessType = Convert.ToString(Session["AccessType"]);
            if (Request.HttpMethod == "POST")
            {
                
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EmployeeLeaves_create", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (oEmployeeLeave.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", oEmployeeLeave.LeaveId);
                            cmd.Parameters.AddWithValue("@EmployeeId", oEmployeeLeave.EmpId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@id", 0);
                            cmd.Parameters.AddWithValue("@EmployeeId", EmpID);
                        }
                        cmd.Parameters.AddWithValue("@sessionId", EmpID);
                        cmd.Parameters.AddWithValue("@LeaveType", oEmployeeLeave.LeaveType);
                        cmd.Parameters.AddWithValue("@EmployeeName", oEmployeeLeave.EmpName);
                        cmd.Parameters.AddWithValue("@StartDate", oEmployeeLeave.startDate);
                        cmd.Parameters.AddWithValue("@EndDate", oEmployeeLeave.EndDate);
                        cmd.Parameters.AddWithValue("@NoOfDays", oEmployeeLeave.NoOfDays);
                        cmd.Parameters.AddWithValue("@LeaveStatus", oEmployeeLeave.LeaveStatus == null ? "" : oEmployeeLeave.LeaveStatus);
                        cmd.Parameters.AddWithValue("@LeaveReason", oEmployeeLeave.LeaveReason == null ? "" : oEmployeeLeave.LeaveReason);
                        cmd.Parameters.AddWithValue("@LeaveBalance", oEmployeeLeave.LeaveBalance);
                        cmd.Parameters.AddWithValue("@ReportingPerson", oEmployeeLeave.Reportingmanager);
                        cmd.Parameters.AddWithValue("@ApproveAction", oEmployeeLeave.ApproveAction == null ? "Pending" : oEmployeeLeave.ApproveAction);
                        cmd.Parameters.AddWithValue("@ReportingManagerId", oEmployeeLeave.ReportingmanagerId);
                        cmd.Parameters.AddWithValue("@CasualLeaveBalance", oEmployeeLeave.CasualLeaveBalance);
                        cmd.Parameters.AddWithValue("@SickLeaveBalance", oEmployeeLeave.SickLeaveBalance);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            if (AccessType != null && AccessType != "User")
            {
                EmpID = Convert.ToString(-2);
            }
            return RedirectToAction("LeaveList", "Leaves", new { id = EmpID, mode = "ApplyList" });
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
        //Get All EmployeeList For Reporting Manager
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
        //Leave Settings List
        [HandleError]
        [HttpGet]
        public ActionResult LeaveSettingsList()
        {
            ViewBag.LeaveSettingList = GetLeaveSettingList();
            return View(ViewBag.LeaveSettingList);
        }

        [HandleError]
        public LeaveSettingHeader GetLeaveSettingList()
        {
            string EmpID = Convert.ToString(Session["Id"]);
            var spName = "";
            LeaveSettingHeader oLeaveSetting = new LeaveSettingHeader();
            List<LeaveSettingHeader> oListLeaveSetting = new List<LeaveSettingHeader>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EmployeeLeaveTypeSettings_Get"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            oLeaveSetting.LeaveSettingList.Add(new LeaveSettingHeader
                            {
                                LeaveTypeId = Convert.ToInt32(sdr["LeaveTypeId"] == DBNull.Value ? 0 : sdr["LeaveTypeId"]),
                                LeaveDescription = Convert.ToString(sdr["description"] == DBNull.Value ? "" : sdr["description"]),
                                LeaveAbbreviation = Convert.ToString(sdr["abbreviation"] == DBNull.Value ? "" : sdr["abbreviation"]),
                                NoOfDays = Convert.ToInt32(sdr["NoOfDays"] == DBNull.Value ? 0 : sdr["NoOfDays"])

                            });
                        }
                        con.Close();
                    }
                }
                ViewBag.leavesettingdetails = oLeaveSetting.LeaveSettingList;
                return oLeaveSetting;
            }

        }

        [HandleError]
        [HttpPost]
        public ActionResult LeaveSettingsList(LeaveSettingHeader oLeaveSettingHeader)
        {

            if (Request.HttpMethod == "POST")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_CreateOrUpdateLeaveSetting", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (LeaveSettingHeader oleaveSettingList in oLeaveSettingHeader.LeaveSettingList)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@id", oleaveSettingList.id);
                            cmd.Parameters.AddWithValue("@LeaveTypeId", oleaveSettingList.LeaveTypeId);
                            cmd.Parameters.AddWithValue("@NoOfDays", oleaveSettingList.NoOfDays);
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }

            return RedirectToAction("LeaveList", "Leaves", new { mode = "ApplyList" });
            // return View();

        }


        //Leave Balance Report List
        [HandleError]
        [HttpGet]
        public ActionResult LeaveBalanceReportList(int id)
        {
            RefreshLeaveReport(id);

            return View(GetLeaveReportList(id));
        }

        [HandleError]
        public LeaveReportHeader GetLeaveReportList(int id)
        {
            LeaveReportHeader leaveReportHeaderList = new LeaveReportHeader();

            string leavebalance = "sp_EmployeeLeaveBalance_Report";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(leavebalance))
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
                            leaveReportHeaderList.LeaveReportList.Add(new LeaveReport
                            {
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                EmployeeName = Convert.ToString(sdr["Name"]),
                                Email = Convert.ToString(sdr["offcEmail"]),
                                FinancialYear = Convert.ToInt32(sdr["FinancialYear"]),
                                OpeningLeaveCasual = Convert.ToDecimal(sdr["OpeningLeaveCasual"]),
                                TakenTillCasual = Convert.ToDecimal(sdr["TakenTillCasual"]),
                                TakenTillSick = Convert.ToDecimal(sdr["TakenTillSick"]),
                                TakenTillToal = Convert.ToDecimal(sdr["TakenTillTotal"]),
                                BalanceCasual = Convert.ToDecimal(sdr["BalanceCasual"]),
                                BalanceSick = Convert.ToDecimal(sdr["BalanceSick"]),
                                EligibleCasual = Convert.ToDecimal(sdr["EligibleCasual"]),
                                EligibleSick = Convert.ToDecimal(sdr["EligibleSick"])
                            });
                        }

                        con2.Close();
                    }
                }
                ViewBag.leavedetails = leaveReportHeaderList.LeaveReportList;
                return leaveReportHeaderList;
            }
        }

        public void RefreshLeaveReport(int id)
        {
            List<Employee> oemployeelist = new List<Employee>();
            string query1 = "";
            string query = "usp_LeaveReport_Calculate";
            if (id == -2)
            {
                query1 = "select id from EmployeeDetails";
            }
            else
            {
                query1 = "select id from EmployeeDetails where id = " + id.ToString();
            }


            //Get All Employee ID's
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            oemployeelist.Add(new Employee
                            {
                                Id = Convert.ToInt32(sdr1["Id"])

                            });
                        }
                    }
                    con1.Close();
                }
            }

            //Refresh List using EMP ID
            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(query))
                {
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    con2.Open();
                    foreach (Employee item in oemployeelist)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@EmpID", item.Id);
                        cmd2.ExecuteNonQuery();
                    }
                    con2.Close();
                }
            }

        }

        public void SendEmailNotification(string FromEmail, string ToEmail)
        {           

            using (var context = new AppEntities1())
            {
                var getFromMail = (from s in context.Enrollments where s.Email == FromEmail select s).FirstOrDefault();
                var getToMail = (from s in context.Enrollments where s.Email == ToEmail select s).FirstOrDefault();
                //string resetURL = "https://localhost:44360/UserLogin/Index";
                string resetURL = "http://122.165.55.128/payroll/UserLogin/Index";
                if (getFromMail != null)
                {                   

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Leave Approve Request";                  

                    var body = "Hi <b>" + getToMail.FirstName
                        + "<br/>You Have Leave Request From Your Team Memeber<br/> '" + FromEmail + "'.<br/>Please Check in Your Payroll Login Aprroval List<br/><a href='" + resetURL + "'>" + resetURL + "</a> <br/><br/> Thank you<br/><b>System Admin</b>.";

                    SendEmail(getToMail.Email, body, subject);

                   ViewBag.Message = "Sent";
                }
                else
                {
                    ViewBag.Message = "Something Went Wrong.";
                }
            }
        }

        [HandleError]
        private void SendEmail(string ToemailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailFrom"], ToemailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                // Add a carbon copy recipient.
                mm.CC.Add("hemalatha90cs@gmail.com");
               // mm.CC.Add("saravanan@zuddhisystems.com");
                SmtpClient smtp = new SmtpClient("smtp.office365.com",587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials =
                new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(mm);
            }
        }

    }
}