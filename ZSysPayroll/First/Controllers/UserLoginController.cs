using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;

namespace First.Controllers
{
    public class UserLoginController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;

        // GET: UserLogin
        [HandleError]
        public ActionResult Index()
        {
            var chkTimeOut = Session.Timeout;
            if (chkTimeOut < 20)
            {
                // set new time out to session  
                Session.Timeout = 60;

            }
            string query = "SELECT [SettingValue]   FROM [Payroll].[dbo].[Settings] where SettingName = 'EmailDomain'";
            Enroll enrolment = new Enroll();
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
                            enrolment.EmailDomain = Convert.ToString(sdr["SettingValue"] == DBNull.Value ? "" : sdr["SettingValue"]);
                        }
                    }
                    con.Close();
                }
            }
            return View(enrolment);
        }

        [HandleError]
        [HttpPost]
        public ActionResult Index(Enroll e)
        {            
            Session["EnrollData"] = e;
            Session["Email"] = "";
            Session["AccessType"] = "";
            string encryptedpassword = EncryptPassword(e.Password);
            String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            SqlConnection con = new SqlConnection(SqlCon);          

            //Attendance
            string query = "select max(a.LogIn) as LastLoginTime,max(a.Logout) as LastLogoutTime from Attendance a join EmployeeDetails e on a.EmployeeId = e.Id RIGHT JOIN Enrollment en on e.offcEmail = en.Email where en.Email = @Email and en.Password = @Password and CONVERT(DATE, a.LogIn) = CONVERT(DATE, GETDATE()) group by e.Name,a.EmployeeId";
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand(query))
                {
                    cmd1.Parameters.AddWithValue("@Email", e.Email);
                    cmd1.Parameters.AddWithValue("@Password", encryptedpassword);

                    cmd1.Connection = con1;
                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {

                        while (sdr1.Read())
                        {
                            Session["LastLoginTime"] = Convert.ToDateTime(sdr1["LastLoginTime"]).ToShortTimeString();
                            Session["LastLogoutTime"] = sdr1["LastLogoutTime"] == DBNull.Value ? "" : Convert.ToDateTime(sdr1["LastLogoutTime"]).ToShortTimeString();
                        }
                    }
                    con1.Close();
                }
            }
            

                string SqlQuery = "select e.id,e.offcEmail,en.Email,en.[Password],en.AccessType FROM EmployeeDetails e RIGHT JOIN Enrollment en on e.offcEmail=en.Email where en.Email=@Email and en.Password=@Password";

            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con);            
            cmd.Parameters.AddWithValue("@Email", e.Email);
            //cmd.Parameters.AddWithValue("@AccessType", e.AccessType);
            cmd.Parameters.AddWithValue("@Password", encryptedpassword);            
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = e.Email.ToString();
                Session["Password"] = encryptedpassword;
                Session["Id"] = Convert.ToString(sdr["Id"]);
                string AccessType = Convert.ToString(sdr["AccessType"]);
                if (string.IsNullOrEmpty(AccessType.Trim()))
                {
                    AccessType = "User";
                }
                Session["AccessType"] = AccessType;

                if(Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
                { 
                return RedirectToAction("Details", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Employee", new { id = Convert.ToInt32(Session["Id"]), mode = 2 });
                }
            }
            else
            {
                ViewData["Message"] = "Username or password do not match";
            }
            //if (e.Email.ToString() != null)
            //{
            //    status = "1";
            //}
            //else
            //{
            //    status = "3";
            //}

            con.Close();
        //}
            return View(e);

        }
        [HandleError]
        public static string EncryptPassword(string strPassword)
        {
            MD5CryptoServiceProvider p = new MD5CryptoServiceProvider();
            byte[] arr = Encoding.UTF8.GetBytes(strPassword);
            arr = p.ComputeHash(arr);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in arr)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        [HandleError]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HandleError]
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            string resetURL = ConfigurationManager.AppSettings["resetURL"];
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "~/UserLogin/ResetPassword/" + resetCode;
            resetURL = resetURL + resetCode;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var existingURL = Request.Url.AbsoluteUri.ToLower();
            var link = existingURL.Replace("/userlogin/forgotpassword", verifyUrl);

            using (var context = new AppEntities1())
            {
                var getUser = (from s in context.Enrollments where s.Email == Email select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;

                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Password Reset Request";
                    link = link.Replace("~", "");

                    var body = "Hi <b>" + getUser.FirstName + "</b>,<br/><br/> You recently requested to reset your password for your account.<br/>"
                        +"<br/>Click the link below to reset it. " + " <br/><a href='" + resetURL + "'>" + resetURL + "</a> <br/><br/>" 
                        + "If you did not request a password reset, please ignore this email or reply to let us now.<br/><br/> Thank you<br/><b>System Admin</b>.";

                    SendEmail(getUser.Email, body, subject);

                    ViewBag.Message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    ViewBag.Message = "User doesn't exists.";
                    return View();
                }
            }

            return View();
        }
        [HandleError]
        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailFrom"], emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);                              
                smtp.Send(mm);
            }
        }
        [HandleError]
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (var context = new AppEntities1())
            {
                var user = context.Enrollments.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPassword model = new ResetPassword();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            string encryptedpassword = EncryptPassword(model.NewPassword);
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new AppEntities1())
                {
                    var user = context.Enrollments.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //you can encrypt password here, we are not doing it
                        user.Password = encryptedpassword;
                        //make resetpasswordcode empty string now
                        user.ResetPasswordCode = "";
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
        [HandleError]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();           
            Session["Email"] = "";
            Session["Id"] ="";
            Session["AccessType"] = "";
            Session.Abandon();
            //disable browsers back buttons.

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));

            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "UserLogin");

        }


        public string StatusRefresh(string sessionemail, string sessionpassword)
        {
            string query = "select max(a.LogIn) as LastLoginTime,max(a.Logout) as LastLogoutTime from Attendance a join EmployeeDetails e on a.EmployeeId = e.Id RIGHT JOIN Enrollment en on e.offcEmail = en.Email where en.Email = @Email and en.Password = @Password and CONVERT(DATE, a.LogIn) = CONVERT(DATE, GETDATE()) group by e.Name,a.EmployeeId";
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand(query))
                {
                    if (sessionemail != null & sessionpassword != null)
                    {
                        cmd1.Parameters.AddWithValue("@Email", sessionemail);
                        cmd1.Parameters.AddWithValue("@Password", sessionpassword);
                    }                    
                    cmd1.Connection = con1;
                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {

                        while (sdr1.Read())
                        {
                            Session["LastLoginTime"] = Convert.ToDateTime(sdr1["LastLoginTime"]).ToShortTimeString();
                            Session["LastLogoutTime"] = sdr1["LastLogoutTime"] == DBNull.Value ? "" : Convert.ToDateTime(sdr1["LastLogoutTime"]).ToShortTimeString();
                        }
                    }
                    con1.Close();
                }
            }
            return "success";
        }
    }
}