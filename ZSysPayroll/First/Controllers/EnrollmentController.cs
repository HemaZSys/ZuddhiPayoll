using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Net;

namespace First.Controllers
{
    public class EnrollmentController : Controller
    {
        // GET: Enrollment
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;

        public string value = "";
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
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
            try { 
            if (Request.HttpMethod == "POST")
            {
                string encryptedpassword = EncryptPassword(e.Password);
                Enroll er = new Enroll();
                
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        bool isEmailOrPhoneExist = false;
                        string checkMailPhoneQry = "SELECT * FROM Enrollment WHERE Email='" + e.Email + "' or Phone='" + e.PhoneNumber + "'";

                        using (SqlCommand cmdCheck = new SqlCommand(checkMailPhoneQry))
                        {
                            cmdCheck.Connection = con;
                            con.Open();
                            using (SqlDataReader sdrCheck = cmdCheck.ExecuteReader())
                            {

                                while (sdrCheck.Read())
                                {
                                    isEmailOrPhoneExist = true;
                                }
                            }

                        }

                        if (!isEmailOrPhoneExist)
                        {
                            using (SqlCommand cmd = new SqlCommand("SP_EnrolDetail", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                                cmd.Parameters.AddWithValue("@LastName", e.LastName);
                                cmd.Parameters.AddWithValue("@Password", encryptedpassword);
                                cmd.Parameters.AddWithValue("@Gender", e.Gender);
                                cmd.Parameters.AddWithValue("@Email", e.Email);
                                cmd.Parameters.AddWithValue("@AccessType", e.AccessType);
                                cmd.Parameters.AddWithValue("@Phone", e.PhoneNumber);
                                cmd.Parameters.AddWithValue("@SecurityAnwser", e.SecurityAnwser);
                                //cmd.Parameters.AddWithValue("@status", "INSERT");
                               // con.Open();
                                ViewData["result"] = cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else
                        {
                           // string paramVal = e.FirstName + "$" + e.LastName + "$" + e.Password + "$" + e.Gender + "$" + e.Email + "$" + e.PhoneNumber;
                            e.errorMessage = "Invalid Email or Mobile. OR Please contact Admin";
                            //ModelState.AddModelError("FormValue", "Yoo");
                            // return RedirectToAction("Index", "Enrollment", new {id = paramVal });
                            return View(e);
                            con.Close();
                        }

                }
            }
            }
            catch (Exception oEx)
            {
                //
            }
            return RedirectToAction("Index", "UserLogin");
            //return View();
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
        [HttpGet]
        public ActionResult EnrollmentList()
        {
            string query = "SELECT [ID], [FirstName], [LastName], [Email], [Phone] FROM [Payroll].[dbo].[Enrollment]";
            List<Enroll> enrolmentList = new List<Enroll>();
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
                            enrolmentList.Add(new Enroll()
                            {
                                FirstName = Convert.ToString(sdr["FirstName"] == DBNull.Value ? "" : sdr["FirstName"]),
                                LastName = Convert.ToString(sdr["LastName"] == DBNull.Value ? "" : sdr["LastName"]),
                                ID = Convert.ToInt32(sdr["ID"] == DBNull.Value ? "" : sdr["ID"]),
                                Email = Convert.ToString(sdr["Email"] == DBNull.Value ? "" : sdr["Email"]),
                                PhoneNumber = Convert.ToString(sdr["Phone"] == DBNull.Value ? "" : sdr["Phone"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(enrolmentList);
        }



        // POST: Home/DeleteEnrollment/5
        [HandleError]
        [HttpGet, ActionName("DeleteEnrollment")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteEnrollment(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);



            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "Delete FROM Enrollment where ID='" + id + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ViewBag.AlertMsg = "Record Deleted Successfully";
                    }
                }
                return RedirectToAction("EnrollmentList");
            }
            catch
            {
                return View();
            }
        }


    }
}