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
using System.Net;
using System.Web.Security;

namespace First.Controllers
{
    public class TaxController : Controller
    {
        //connection string to connect to data base.
        string constr = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;

        // GET: Tax
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Form80CSectionlist = GetLookup("Form80CSection");
            return View();
        }

        //Download file
        [HandleError]
        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
        [HandleError]
        [HttpGet]
        public ActionResult CreateForm80CSettings()
        {
            ViewBag.Form80CSectionlist = GetLookup("Form80CSection");
            return View();
        }

        // GET: Tax
        //[HttpGet]
        //    public ActionResult UploadFile()
        //    {
        //        return View();
        //    }
        //    [HttpPost]
        //    public ActionResult UploadFile(HttpPostedFileBase file)
        //    {
        //        try
        //        {
        //            if (file.ContentLength > 0)
        //            {
        //                string _FileName = Path.GetFileName(file.FileName);
        //                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
        //                file.SaveAs(_path);
        //            }
        //            ViewBag.Message = "File Uploaded Successfully!!";
        //            return View();
        //        }
        //        catch
        //        {
        //            ViewBag.Message = "File upload failed!!";
        //            return View();
        //        }
        //    }
        //}
        // GET: Tax
        [HandleError]
        [HttpGet]
        public ActionResult Form80C(int id, DateTime year)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.ToList();



                if (employees != null)
                {
                    ViewBag.employees = employees;
                }
            }
            Form80CHeader form80CHeader = new Form80CHeader();
            List<_TaxDeclFormSetting> _TaxDeclFormSettings = new List<_TaxDeclFormSetting>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deductions_Get"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.AprilMetro = Convert.ToString(sdr["AprilMetro"]);
                            form80CHeader.MayMetro = Convert.ToString(sdr["MayMetro"]);
                            form80CHeader.JuneMetro = Convert.ToString(sdr["JuneMetro"]);
                            form80CHeader.JulyMetro = Convert.ToString(sdr["JulyMetro"]);
                            form80CHeader.AugustMetro = Convert.ToString(sdr["AugustMetro"]);
                            form80CHeader.SeptemberMetro = Convert.ToString(sdr["SeptemberMetro"]);
                            form80CHeader.OctoberMetro = Convert.ToString(sdr["OctoberMetro"]);
                            form80CHeader.NovemberMetro = Convert.ToString(sdr["NovemberMetro"]);
                            form80CHeader.DecemberMetro = Convert.ToString(sdr["DecemberMetro"]);
                            form80CHeader.JanuaryMetro = Convert.ToString(sdr["JanuaryMetro"]);
                            form80CHeader.FebruaryMetro = Convert.ToString(sdr["FebruaryMetro"]);
                            form80CHeader.MarchMetro = Convert.ToString(sdr["MarchMetro"]);
                            form80CHeader.AprilProofSubmission = Convert.ToString(sdr["AprilProofSubmission"]);
                            form80CHeader.MayProofSubmission = Convert.ToString(sdr["MayProofSubmission"]);
                            form80CHeader.JuneProofSubmission = Convert.ToString(sdr["JuneProofSubmission"]);
                            form80CHeader.JulyProofSubmission = Convert.ToString(sdr["JulyProofSubmission"]);
                            form80CHeader.AugustProofSubmission = Convert.ToString(sdr["AugustProofSubmission"]);
                            form80CHeader.SeptemberProofSubmission = Convert.ToString(sdr["SeptemberProofSubmission"]);
                            form80CHeader.OctoberProofSubmission = Convert.ToString(sdr["OctoberProofSubmission"]);
                            form80CHeader.NovemberProofSubmission = Convert.ToString(sdr["NovemberProofSubmission"]);
                            form80CHeader.DecemberProofSubmission = Convert.ToString(sdr["DecemberProofSubmission"]);
                            form80CHeader.JanuaryProofSubmission = Convert.ToString(sdr["JanuaryProofSubmission"]);
                            form80CHeader.FebruaryProofSubmission = Convert.ToString(sdr["FebruaryProofSubmission"]);
                            form80CHeader.MarchProofSubmission = Convert.ToString(sdr["MarchProofSubmission"]);
                            form80CHeader.AprilNonMetro = Convert.ToString(sdr["AprilNonMetro"]);
                            form80CHeader.MayNonMetro = Convert.ToString(sdr["MayNonMetro"]);
                            form80CHeader.JuneNonMetro = Convert.ToString(sdr["JuneNonMetro"]);
                            form80CHeader.JulyNonMetro = Convert.ToString(sdr["JulyNonMetro"]);
                            form80CHeader.AugustNonMetro = Convert.ToString(sdr["AugustNonMetro"]);
                            form80CHeader.SeptemberNonMetro = Convert.ToString(sdr["SeptemberNonMetro"]);
                            form80CHeader.OctoberNonMetro = Convert.ToString(sdr["OctoberNonMetro"]);
                            form80CHeader.NovemberNonMetro = Convert.ToString(sdr["NovemberNonMetro"]);
                            form80CHeader.DecemberNonMetro = Convert.ToString(sdr["DecemberNonMetro"]);
                            form80CHeader.JanuaryNonMetro = Convert.ToString(sdr["JanuaryNonMetro"]);
                            form80CHeader.FebruaryNonMetro = Convert.ToString(sdr["FebruaryNonMetro"]);
                            form80CHeader.MarchNonMetro = Convert.ToString(sdr["MarchNonMetro"]);
                            form80CHeader.AprilNonMetroProofSubmission = Convert.ToString(sdr["AprilNonMetroProofSubmission"]);
                            form80CHeader.MayNonMetroProofSubmission = Convert.ToString(sdr["MayNonMetroProofSubmission"]);
                            form80CHeader.JuneNonMetroProofSubmission = Convert.ToString(sdr["JuneNonMetroProofSubmission"]);
                            form80CHeader.JulyNonMetroProofSubmission = Convert.ToString(sdr["JulyNonMetroProofSubmission"]);
                            form80CHeader.AugustNonMetroProofSubmission = Convert.ToString(sdr["AugustNonMetroProofSubmission"]);
                            form80CHeader.SeptemberNonMetroProofSubmission = Convert.ToString(sdr["SeptemberNonMetroProofSubmission"]);
                            form80CHeader.OctoberNonMetroProofSubmission = Convert.ToString(sdr["OctoberNonMetroProofSubmission"]);
                            form80CHeader.NovemberNonMetroProofSubmission = Convert.ToString(sdr["NovemberNonMetroProofSubmission"]);
                            form80CHeader.DecemberNonMetroProofSubmission = Convert.ToString(sdr["DecemberNonMetroProofSubmission"]);
                            form80CHeader.JanuaryNonMetroProofSubmission = Convert.ToString(sdr["JanuaryNonMetroProofSubmission"]);
                            form80CHeader.FebruaryNonMetroProofSubmission = Convert.ToString(sdr["FebruaryNonMetroProofSubmission"]);
                            form80CHeader.MarchNonMetroProofSubmission = Convert.ToString(sdr["MarchNonMetroProofSubmission"]);
                            form80CHeader.AprilRemarks = Convert.ToString(sdr["AprilRemarks"]);
                            form80CHeader.MayRemarks = Convert.ToString(sdr["MayRemarks"]);
                            form80CHeader.JuneRemarks = Convert.ToString(sdr["JuneRemarks"]);
                            form80CHeader.JulyRemarks = Convert.ToString(sdr["JulyRemarks"]);
                            form80CHeader.AugustRemarks = Convert.ToString(sdr["AugustRemarks"]);
                            form80CHeader.SeptemberRemarks = Convert.ToString(sdr["SeptemberRemarks"]);
                            form80CHeader.OctoberRemarks = Convert.ToString(sdr["OctoberRemarks"]);
                            form80CHeader.NovemberRemarks = Convert.ToString(sdr["NovemberRemarks"]);
                            form80CHeader.DecemberRemarks = Convert.ToString(sdr["DecemberRemarks"]);
                            form80CHeader.JanuaryRemarks = Convert.ToString(sdr["JanuaryRemarks"]);
                            form80CHeader.FebruaryRemarks = Convert.ToString(sdr["FebruaryRemarks"]);
                            form80CHeader.MarchRemarks = Convert.ToString(sdr["MarchRemarks"]);
                            form80CHeader.AprilLandlordPAN = Convert.ToString(sdr["AprilLandlordPAN"]);
                            form80CHeader.MayLandlordPAN = Convert.ToString(sdr["MayLandlordPAN"]);
                            form80CHeader.JuneLandlordPAN = Convert.ToString(sdr["JuneLandlordPAN"]);
                            form80CHeader.JulyLandlordPAN = Convert.ToString(sdr["JulyLandlordPAN"]);
                            form80CHeader.AugustLandlordPAN = Convert.ToString(sdr["AugustLandlordPAN"]);
                            form80CHeader.SeptemberLandlordPAN = Convert.ToString(sdr["SeptemberLandlordPAN"]);
                            form80CHeader.OctoberLandlordPAN = Convert.ToString(sdr["OctoberLandlordPAN"]);
                            form80CHeader.NovemberLandlordPAN = Convert.ToString(sdr["NovemberLandlordPAN"]);
                            form80CHeader.DecemberLandlordPAN = Convert.ToString(sdr["DecemberLandlordPAN"]);
                            form80CHeader.JanuaryLandlordPAN = Convert.ToString(sdr["JanuaryLandlordPAN"]);
                            form80CHeader.FebruaryLandlordPAN = Convert.ToString(sdr["FebruaryLandlordPAN"]);
                            form80CHeader.MarchLandlordPAN = Convert.ToString(sdr["MarchLandlordPAN"]);
                            form80CHeader.AprilLandlordName = Convert.ToString(sdr["AprilLandlordName"]);
                            form80CHeader.MayLandlordName = Convert.ToString(sdr["MayLandlordName"]);
                            form80CHeader.JuneLandlordName = Convert.ToString(sdr["JuneLandlordName"]);
                            form80CHeader.JulyLandlordName = Convert.ToString(sdr["JulyLandlordName"]);
                            form80CHeader.AugustLandlordName = Convert.ToString(sdr["AugustLandlordName"]);
                            form80CHeader.SeptemberLandlordName = Convert.ToString(sdr["SeptemberLandlordName"]);
                            form80CHeader.OctoberLandlordName = Convert.ToString(sdr["OctoberLandlordName"]);
                            form80CHeader.NovemberLandlordName = Convert.ToString(sdr["NovemberLandlordName"]);
                            form80CHeader.DecemberLandlordName = Convert.ToString(sdr["DecemberLandlordName"]);
                            form80CHeader.JanuaryLandlordName = Convert.ToString(sdr["JanuaryLandlordName"]);
                            form80CHeader.FebruaryLandlordName = Convert.ToString(sdr["FebruaryLandlordName"]);
                            form80CHeader.MarchLandlordName = Convert.ToString(sdr["MarchLandlordName"]);
                            form80CHeader.AprilLandlordAddress = Convert.ToString(sdr["AprilLandlordAddress"]);
                            form80CHeader.MayLandlordAddress = Convert.ToString(sdr["MayLandlordAddress"]);
                            form80CHeader.JuneLandlordAddress = Convert.ToString(sdr["JuneLandlordAddress"]);
                            form80CHeader.JulyLandlordAddress = Convert.ToString(sdr["JulyLandlordAddress"]);
                            form80CHeader.AugustLandlordAddress = Convert.ToString(sdr["AugustLandlordAddress"]);
                            form80CHeader.SeptemberLandlordAddress = Convert.ToString(sdr["SeptemberLandlordAddress"]);
                            form80CHeader.OctoberLandlordAddress = Convert.ToString(sdr["OctoberLandlordAddress"]);
                            form80CHeader.NovemberLandlordAddress = Convert.ToString(sdr["NovemberLandlordAddress"]);
                            form80CHeader.DecemberLandlordAddress = Convert.ToString(sdr["DecemberLandlordAddress"]);
                            form80CHeader.JanuaryLandlordAddress = Convert.ToString(sdr["JanuaryLandlordAddress"]);
                            form80CHeader.FebruaryLandlordAddress = Convert.ToString(sdr["FebruaryLandlordAddress"]);
                            form80CHeader.MarchLandlordAddress = Convert.ToString(sdr["MarchLandlordAddress"]);
                            form80CHeader.RentProofFile = Convert.ToString(sdr["RentProofFile"]);
                            form80CHeader.MonthYear = Convert.ToDateTime(sdr["monthyear"] == DBNull.Value ? year : sdr["monthyear"]);

                        }
                    }
                }
            }

            List<Form80C> form80CList = new List<Form80C>();

            string query = "sp_deductionsLine_Get";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80C form80C = new Form80C();
                            _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();

                            _TaxDeclFormSetting.Id = Convert.ToInt32(sdr["Id"]);
                            form80C.Form80CsettingId = Convert.ToInt32(sdr["Id"]);

                            _TaxDeclFormSetting.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);
                            form80C.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);

                            _TaxDeclFormSetting.Description = Convert.ToString(sdr["Description"]);
                            form80C.DescriptionLabel = Convert.ToString(sdr["Description"]);

                            _TaxDeclFormSetting.ControlType = Convert.ToInt32(sdr["ControlType"]);
                            //form80C.ControlType = Convert.ToInt32(sdr["ControlType"]);

                            _TaxDeclFormSetting.LineNumber = Convert.ToString(sdr["LineNumber"]);
                            form80C.LineNumber = Convert.ToString(sdr["LineNumber"]);

                            _TaxDeclFormSetting.Section = Convert.ToString(sdr["sectiondescription"]);
                            form80C.Section = Convert.ToString(sdr["sectiondescription"]);

                            _TaxDeclFormSetting.SectionName = Convert.ToString(sdr["SectionName"]);
                            form80C.SectionName = Convert.ToString(sdr["SectionName"]);

                            _TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                            //form80C.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);

                            _TaxDeclFormSetting.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);
                            //form80C.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);

                            _TaxDeclFormSetting.AttachmentType = Convert.ToString(sdr["AttachmentType"]);
                            //form80C.AttachmentType = Convert.ToString(sdr["AttachmentType"]);

                            _TaxDeclFormSetting.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);
                            //form80C.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);


                            //_TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                            form80C.Declared_Amt = Convert.ToDecimal(sdr["Declared_Amt"]);

                            //_TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                            form80C.Proof_Amt = Convert.ToDecimal(sdr["Proof_Amt"]);
                            form80C.Proof_file_location = Convert.ToString(sdr["Proof_file"]);
                            form80C.Description = Convert.ToString(sdr["Value"]);

                            form80C.MonthYear = Convert.ToDateTime(sdr["monthyear"] == DBNull.Value ? year : sdr["monthyear"]);

                            _TaxDeclFormSettings.Add(_TaxDeclFormSetting);
                            form80CHeader.Form80CList.Add(form80C);
                        }
                    }
                    con.Close();
                }
            }
            if (_TaxDeclFormSettings.Count == 0)
            {
                _TaxDeclFormSettings.Add(new _TaxDeclFormSetting());
            }
            if (form80CHeader.Form80CList.Count == 0)
            {
                form80CHeader.Form80CList.Add(new Form80C());
            }

            ViewBag.SettingsTaxDeclForm = _TaxDeclFormSettings;
            return View(form80CHeader);
        }

        //Add Form
        [HttpPost]
        [HandleError]
        [ActionName("Form80C")]
        public ActionResult Form80C(Form80CHeader form80CHeader)
        {
            int headerid = 0;
            if (Request.HttpMethod == "POST")
            {
                string RentProof = SaveToPhysicalLocation(form80CHeader.RentProofFileBase, form80CHeader.Empid, DateTime.Now);
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormHeader_create", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Empid", form80CHeader.Empid);
                        cmd.Parameters.AddWithValue("@Employeeid", form80CHeader.Employeeid);
                        cmd.Parameters.AddWithValue("@AprilMetro", form80CHeader.AprilMetro);
                        cmd.Parameters.AddWithValue("@MayMetro", form80CHeader.MayMetro);
                        cmd.Parameters.AddWithValue("@JuneMetro", form80CHeader.JuneMetro);
                        cmd.Parameters.AddWithValue("@JulyMetro", form80CHeader.JulyMetro);
                        cmd.Parameters.AddWithValue("@AugustMetro", form80CHeader.AugustMetro);
                        cmd.Parameters.AddWithValue("@SeptemberMetro", form80CHeader.SeptemberMetro);
                        cmd.Parameters.AddWithValue("@OctoberMetro", form80CHeader.OctoberMetro);
                        cmd.Parameters.AddWithValue("@NovemberMetro", form80CHeader.NovemberMetro);
                        cmd.Parameters.AddWithValue("@DecemberMetro", form80CHeader.DecemberMetro);
                        cmd.Parameters.AddWithValue("@JanuaryMetro", form80CHeader.JanuaryMetro);
                        cmd.Parameters.AddWithValue("@FebruaryMetro", form80CHeader.FebruaryMetro);
                        cmd.Parameters.AddWithValue("@MarchMetro", form80CHeader.MarchMetro);
                        cmd.Parameters.AddWithValue("@AprilProofSubmission", form80CHeader.AprilProofSubmission);
                        cmd.Parameters.AddWithValue("@MayProofSubmission", form80CHeader.MayProofSubmission);
                        cmd.Parameters.AddWithValue("@JuneProofSubmission", form80CHeader.JuneProofSubmission);
                        cmd.Parameters.AddWithValue("@JulyProofSubmission", form80CHeader.JulyProofSubmission);
                        cmd.Parameters.AddWithValue("@AugustProofSubmission", form80CHeader.AugustProofSubmission);
                        cmd.Parameters.AddWithValue("@SeptemberProofSubmission", form80CHeader.SeptemberProofSubmission);
                        cmd.Parameters.AddWithValue("@OctoberProofSubmission", form80CHeader.OctoberProofSubmission);
                        cmd.Parameters.AddWithValue("@NovemberProofSubmission", form80CHeader.NovemberProofSubmission);
                        cmd.Parameters.AddWithValue("@DecemberProofSubmission", form80CHeader.DecemberProofSubmission);
                        cmd.Parameters.AddWithValue("@JanuaryProofSubmission", form80CHeader.JanuaryProofSubmission);
                        cmd.Parameters.AddWithValue("@FebruaryProofSubmission", form80CHeader.FebruaryProofSubmission);
                        cmd.Parameters.AddWithValue("@MarchProofSubmission", form80CHeader.MarchProofSubmission);
                        cmd.Parameters.AddWithValue("@AprilNonMetro", form80CHeader.AprilNonMetro);
                        cmd.Parameters.AddWithValue("@MayNonMetro", form80CHeader.MayNonMetro);
                        cmd.Parameters.AddWithValue("@JuneNonMetro", form80CHeader.JuneNonMetro);
                        cmd.Parameters.AddWithValue("@JulyNonMetro", form80CHeader.JulyNonMetro);
                        cmd.Parameters.AddWithValue("@AugustNonMetro", form80CHeader.AugustNonMetro);
                        cmd.Parameters.AddWithValue("@SeptemberNonMetro", form80CHeader.SeptemberNonMetro);
                        cmd.Parameters.AddWithValue("@OctoberNonMetro", form80CHeader.OctoberNonMetro);
                        cmd.Parameters.AddWithValue("@NovemberNonMetro", form80CHeader.NovemberNonMetro);
                        cmd.Parameters.AddWithValue("@DecemberNonMetro", form80CHeader.DecemberNonMetro);
                        cmd.Parameters.AddWithValue("@JanuaryNonMetro", form80CHeader.JanuaryNonMetro);
                        cmd.Parameters.AddWithValue("@FebruaryNonMetro", form80CHeader.FebruaryNonMetro);
                        cmd.Parameters.AddWithValue("@MarchNonMetro", form80CHeader.MarchNonMetro);
                        cmd.Parameters.AddWithValue("@AprilNonMetroProofSubmission", form80CHeader.AprilNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@MayNonMetroProofSubmission", form80CHeader.MayNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@JuneNonMetroProofSubmission", form80CHeader.JuneNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@JulyNonMetroProofSubmission", form80CHeader.JulyNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@AugustNonMetroProofSubmission", form80CHeader.AugustNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@SeptemberNonMetroProofSubmission", form80CHeader.SeptemberNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@OctoberNonMetroProofSubmission", form80CHeader.OctoberNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@NovemberNonMetroProofSubmission", form80CHeader.NovemberNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@DecemberNonMetroProofSubmission", form80CHeader.DecemberNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@JanuaryNonMetroProofSubmission", form80CHeader.JanuaryNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@FebruaryNonMetroProofSubmission", form80CHeader.FebruaryNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@MarchNonMetroProofSubmission", form80CHeader.MarchNonMetroProofSubmission);
                        cmd.Parameters.AddWithValue("@AprilRemarks", form80CHeader.AprilRemarks);
                        cmd.Parameters.AddWithValue("@MayRemarks", form80CHeader.MayRemarks);
                        cmd.Parameters.AddWithValue("@JuneRemarks", form80CHeader.JuneRemarks);
                        cmd.Parameters.AddWithValue("@JulyRemarks", form80CHeader.JulyRemarks);
                        cmd.Parameters.AddWithValue("@AugustRemarks", form80CHeader.AugustRemarks);
                        cmd.Parameters.AddWithValue("@SeptemberRemarks", form80CHeader.SeptemberRemarks);
                        cmd.Parameters.AddWithValue("@OctoberRemarks", form80CHeader.OctoberRemarks);
                        cmd.Parameters.AddWithValue("@NovemberRemarks", form80CHeader.NovemberRemarks);
                        cmd.Parameters.AddWithValue("@DecemberRemarks", form80CHeader.DecemberRemarks);
                        cmd.Parameters.AddWithValue("@JanuaryRemarks", form80CHeader.JanuaryRemarks);
                        cmd.Parameters.AddWithValue("@FebruaryRemarks", form80CHeader.FebruaryRemarks);
                        cmd.Parameters.AddWithValue("@MarchRemarks", form80CHeader.MarchRemarks);
                        cmd.Parameters.AddWithValue("@AprilLandlordPAN", form80CHeader.AprilLandlordPAN);
                        cmd.Parameters.AddWithValue("@MayLandlordPAN", form80CHeader.MayLandlordPAN);
                        cmd.Parameters.AddWithValue("@JuneLandlordPAN", form80CHeader.JuneLandlordPAN);
                        cmd.Parameters.AddWithValue("@JulyLandlordPAN", form80CHeader.JulyLandlordPAN);
                        cmd.Parameters.AddWithValue("@AugustLandlordPAN", form80CHeader.AugustLandlordPAN);
                        cmd.Parameters.AddWithValue("@SeptemberLandlordPAN", form80CHeader.SeptemberLandlordPAN);
                        cmd.Parameters.AddWithValue("@OctoberLandlordPAN", form80CHeader.OctoberLandlordPAN);
                        cmd.Parameters.AddWithValue("@NovemberLandlordPAN", form80CHeader.NovemberLandlordPAN);
                        cmd.Parameters.AddWithValue("@DecemberLandlordPAN", form80CHeader.DecemberLandlordPAN);
                        cmd.Parameters.AddWithValue("@JanuaryLandlordPAN", form80CHeader.JanuaryLandlordPAN);
                        cmd.Parameters.AddWithValue("@FebruaryLandlordPAN", form80CHeader.FebruaryLandlordPAN);
                        cmd.Parameters.AddWithValue("@MarchLandlordPAN", form80CHeader.MarchLandlordPAN);
                        cmd.Parameters.AddWithValue("@AprilLandlordName", form80CHeader.AprilLandlordName);
                        cmd.Parameters.AddWithValue("@MayLandlordName", form80CHeader.MayLandlordName);
                        cmd.Parameters.AddWithValue("@JuneLandlordName", form80CHeader.JuneLandlordName);
                        cmd.Parameters.AddWithValue("@JulyLandlordName", form80CHeader.JulyLandlordName);
                        cmd.Parameters.AddWithValue("@AugustLandlordName", form80CHeader.AugustLandlordName);
                        cmd.Parameters.AddWithValue("@SeptemberLandlordName", form80CHeader.SeptemberLandlordName);
                        cmd.Parameters.AddWithValue("@OctoberLandlordName", form80CHeader.OctoberLandlordName);
                        cmd.Parameters.AddWithValue("@NovemberLandlordName", form80CHeader.NovemberLandlordName);
                        cmd.Parameters.AddWithValue("@DecemberLandlordName", form80CHeader.DecemberLandlordName);
                        cmd.Parameters.AddWithValue("@JanuaryLandlordName", form80CHeader.JanuaryLandlordName);
                        cmd.Parameters.AddWithValue("@FebruaryLandlordName", form80CHeader.FebruaryLandlordName);
                        cmd.Parameters.AddWithValue("@MarchLandlordName", form80CHeader.MarchLandlordName);
                        cmd.Parameters.AddWithValue("@AprilLandlordAddress", form80CHeader.AprilLandlordAddress);
                        cmd.Parameters.AddWithValue("@MayLandlordAddress", form80CHeader.MayLandlordAddress);
                        cmd.Parameters.AddWithValue("@JuneLandlordAddress", form80CHeader.JuneLandlordAddress);
                        cmd.Parameters.AddWithValue("@JulyLandlordAddress", form80CHeader.JulyLandlordAddress);
                        cmd.Parameters.AddWithValue("@AugustLandlordAddress", form80CHeader.AugustLandlordAddress);
                        cmd.Parameters.AddWithValue("@SeptemberLandlordAddress", form80CHeader.SeptemberLandlordAddress);
                        cmd.Parameters.AddWithValue("@OctoberLandlordAddress", form80CHeader.OctoberLandlordAddress);
                        cmd.Parameters.AddWithValue("@NovemberLandlordAddress", form80CHeader.NovemberLandlordAddress);
                        cmd.Parameters.AddWithValue("@DecemberLandlordAddress", form80CHeader.DecemberLandlordAddress);
                        cmd.Parameters.AddWithValue("@JanuaryLandlordAddress", form80CHeader.JanuaryLandlordAddress);
                        cmd.Parameters.AddWithValue("@FebruaryLandlordAddress", form80CHeader.FebruaryLandlordAddress);
                        cmd.Parameters.AddWithValue("@MarchLandlordAddress", form80CHeader.MarchLandlordAddress);
                        cmd.Parameters.AddWithValue("@RentProofFile", RentProof);
                        cmd.Parameters.AddWithValue("@monthyear", form80CHeader.MonthYear);

                        con.Open();
                        headerid = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Parameters.Clear();
                        con.Close();
                    }


                    using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_create", con))
                    {
                        foreach (Form80C form80C in form80CHeader.Form80CList)
                        {
                            string Proof_file = SaveToPhysicalLocation(form80C.Proof_file, form80CHeader.Empid, DateTime.Now);
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@id", form80CHeader.headerid);
                            cmd.Parameters.AddWithValue("@TaxDeclFormid", form80C.TaxId);
                            cmd.Parameters.AddWithValue("@TaxDeclFormSettingid", form80C.Form80CsettingId);
                            cmd.Parameters.AddWithValue("@Value", form80C.Description != null ? form80C.Description : form80C.Descriptioncheckbox.ToString());
                            cmd.Parameters.AddWithValue("@Declared_Amt", form80C.Declared_Amt);
                            cmd.Parameters.AddWithValue("@Proof_Amt", form80C.Proof_Amt);
                            cmd.Parameters.AddWithValue("@Proof_file", Proof_file);
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@EmployeeId", form80CHeader.Empid);
                            cmd.Parameters.AddWithValue("@UserId", "");
                            cmd.Parameters.AddWithValue("@Form80CHeaderid", headerid);
                            cmd.Parameters.AddWithValue("@monthyear", form80CHeader.MonthYear);
                            con.Open();
                            int id = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            con.Close();
                        }

                    }
                }
            }

            return RedirectToAction("ITCalculation", "Tax", new { id = form80CHeader.Empid });

            //if (Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
            //{
            //    return RedirectToAction("Details", "Employee");
            //}
            //else
            //{
            //    return RedirectToAction("EmployeeDetails", "Employee", new { id = form80CHeader.Empid });
            //}
        }

        // 3. *************Update Form80Cdeductions Detail ******************

        // GET: Home/UpdateForm80Cdeduction/5
        [HandleError]
        public ActionResult UpdateForm80Cdeduction(int? id, DateTime year)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form80CHeader form80CHeader = new Form80CHeader();


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deductionheader_get"))
                {
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                form80CHeader.CreatedDate = year;
                                form80CHeader.headerid = Convert.ToInt32(sdr["headerid"]);
                                //form80CHeader.headerid = Convert.ToInt32(sdr["Id"]);
                                form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                                form80CHeader.Empid = Convert.ToInt32(id);
                                form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                                form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                                form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                                form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                                form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                                form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                                form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                                form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                                form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                                form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                                form80CHeader.AprilMetro = Convert.ToString(sdr["AprilMetro"]);

                                form80CHeader.MayMetro = Convert.ToString(sdr["MayMetro"]);
                                form80CHeader.JuneMetro = Convert.ToString(sdr["JuneMetro"]);
                                form80CHeader.JulyMetro = Convert.ToString(sdr["JulyMetro"]);
                                form80CHeader.AugustMetro = Convert.ToString(sdr["AugustMetro"]);
                                form80CHeader.SeptemberMetro = Convert.ToString(sdr["SeptemberMetro"]);
                                form80CHeader.OctoberMetro = Convert.ToString(sdr["OctoberMetro"]);
                                form80CHeader.NovemberMetro = Convert.ToString(sdr["NovemberMetro"]);
                                form80CHeader.DecemberMetro = Convert.ToString(sdr["DecemberMetro"]);
                                form80CHeader.JanuaryMetro = Convert.ToString(sdr["JanuaryMetro"]);
                                form80CHeader.FebruaryMetro = Convert.ToString(sdr["FebruaryMetro"]);
                                form80CHeader.MarchMetro = Convert.ToString(sdr["MarchMetro"]);
                                form80CHeader.AprilProofSubmission = Convert.ToString(sdr["AprilProofSubmission"]);
                                form80CHeader.MayProofSubmission = Convert.ToString(sdr["MayProofSubmission"]);
                                form80CHeader.JuneProofSubmission = Convert.ToString(sdr["JuneProofSubmission"]);
                                form80CHeader.JulyProofSubmission = Convert.ToString(sdr["JulyProofSubmission"]);
                                form80CHeader.AugustProofSubmission = Convert.ToString(sdr["AugustProofSubmission"]);
                                form80CHeader.SeptemberProofSubmission = Convert.ToString(sdr["SeptemberProofSubmission"]);
                                form80CHeader.OctoberProofSubmission = Convert.ToString(sdr["OctoberProofSubmission"]);
                                form80CHeader.NovemberProofSubmission = Convert.ToString(sdr["NovemberProofSubmission"]);
                                form80CHeader.DecemberProofSubmission = Convert.ToString(sdr["DecemberProofSubmission"]);
                                form80CHeader.JanuaryProofSubmission = Convert.ToString(sdr["JanuaryProofSubmission"]);
                                form80CHeader.FebruaryProofSubmission = Convert.ToString(sdr["FebruaryProofSubmission"]);
                                form80CHeader.MarchProofSubmission = Convert.ToString(sdr["MarchProofSubmission"]);
                                form80CHeader.AprilNonMetro = Convert.ToString(sdr["AprilNonMetro"]);
                                form80CHeader.MayNonMetro = Convert.ToString(sdr["MayNonMetro"]);
                                form80CHeader.JuneNonMetro = Convert.ToString(sdr["JuneNonMetro"]);
                                form80CHeader.JulyNonMetro = Convert.ToString(sdr["JulyNonMetro"]);
                                form80CHeader.AugustNonMetro = Convert.ToString(sdr["AugustNonMetro"]);
                                form80CHeader.SeptemberNonMetro = Convert.ToString(sdr["SeptemberNonMetro"]);
                                form80CHeader.OctoberNonMetro = Convert.ToString(sdr["OctoberNonMetro"]);
                                form80CHeader.NovemberNonMetro = Convert.ToString(sdr["NovemberNonMetro"]);
                                form80CHeader.DecemberNonMetro = Convert.ToString(sdr["DecemberNonMetro"]);
                                form80CHeader.JanuaryNonMetro = Convert.ToString(sdr["JanuaryNonMetro"]);
                                form80CHeader.FebruaryNonMetro = Convert.ToString(sdr["FebruaryNonMetro"]);
                                form80CHeader.MarchNonMetro = Convert.ToString(sdr["MarchNonMetro"]);
                                form80CHeader.AprilNonMetroProofSubmission = Convert.ToString(sdr["AprilNonMetroProofSubmission"]);
                                form80CHeader.MayNonMetroProofSubmission = Convert.ToString(sdr["MayNonMetroProofSubmission"]);
                                form80CHeader.JuneNonMetroProofSubmission = Convert.ToString(sdr["JuneNonMetroProofSubmission"]);
                                form80CHeader.JulyNonMetroProofSubmission = Convert.ToString(sdr["JulyNonMetroProofSubmission"]);
                                form80CHeader.AugustNonMetroProofSubmission = Convert.ToString(sdr["AugustNonMetroProofSubmission"]);
                                form80CHeader.SeptemberNonMetroProofSubmission = Convert.ToString(sdr["SeptemberNonMetroProofSubmission"]);
                                form80CHeader.OctoberNonMetroProofSubmission = Convert.ToString(sdr["OctoberNonMetroProofSubmission"]);
                                form80CHeader.NovemberNonMetroProofSubmission = Convert.ToString(sdr["NovemberNonMetroProofSubmission"]);
                                form80CHeader.DecemberNonMetroProofSubmission = Convert.ToString(sdr["DecemberNonMetroProofSubmission"]);
                                form80CHeader.JanuaryNonMetroProofSubmission = Convert.ToString(sdr["JanuaryNonMetroProofSubmission"]);
                                form80CHeader.FebruaryNonMetroProofSubmission = Convert.ToString(sdr["FebruaryNonMetroProofSubmission"]);
                                form80CHeader.MarchNonMetroProofSubmission = Convert.ToString(sdr["MarchNonMetroProofSubmission"]);
                                form80CHeader.AprilRemarks = Convert.ToString(sdr["AprilRemarks"]);
                                form80CHeader.MayRemarks = Convert.ToString(sdr["MayRemarks"]);
                                form80CHeader.JuneRemarks = Convert.ToString(sdr["JuneRemarks"]);
                                form80CHeader.JulyRemarks = Convert.ToString(sdr["JulyRemarks"]);
                                form80CHeader.AugustRemarks = Convert.ToString(sdr["AugustRemarks"]);
                                form80CHeader.SeptemberRemarks = Convert.ToString(sdr["SeptemberRemarks"]);
                                form80CHeader.OctoberRemarks = Convert.ToString(sdr["OctoberRemarks"]);
                                form80CHeader.NovemberRemarks = Convert.ToString(sdr["NovemberRemarks"]);
                                form80CHeader.DecemberRemarks = Convert.ToString(sdr["DecemberRemarks"]);
                                form80CHeader.JanuaryRemarks = Convert.ToString(sdr["JanuaryRemarks"]);
                                form80CHeader.FebruaryRemarks = Convert.ToString(sdr["FebruaryRemarks"]);
                                form80CHeader.MarchRemarks = Convert.ToString(sdr["MarchRemarks"]);
                                form80CHeader.AprilLandlordPAN = Convert.ToString(sdr["AprilLandlordPAN"]);
                                form80CHeader.MayLandlordPAN = Convert.ToString(sdr["MayLandlordPAN"]);
                                form80CHeader.JuneLandlordPAN = Convert.ToString(sdr["JuneLandlordPAN"]);
                                form80CHeader.JulyLandlordPAN = Convert.ToString(sdr["JulyLandlordPAN"]);
                                form80CHeader.AugustLandlordPAN = Convert.ToString(sdr["AugustLandlordPAN"]);
                                form80CHeader.SeptemberLandlordPAN = Convert.ToString(sdr["SeptemberLandlordPAN"]);
                                form80CHeader.OctoberLandlordPAN = Convert.ToString(sdr["OctoberLandlordPAN"]);
                                form80CHeader.NovemberLandlordPAN = Convert.ToString(sdr["NovemberLandlordPAN"]);
                                form80CHeader.DecemberLandlordPAN = Convert.ToString(sdr["DecemberLandlordPAN"]);
                                form80CHeader.JanuaryLandlordPAN = Convert.ToString(sdr["JanuaryLandlordPAN"]);
                                form80CHeader.FebruaryLandlordPAN = Convert.ToString(sdr["FebruaryLandlordPAN"]);
                                form80CHeader.MarchLandlordPAN = Convert.ToString(sdr["MarchLandlordPAN"]);
                                form80CHeader.AprilLandlordName = Convert.ToString(sdr["AprilLandlordName"]);
                                form80CHeader.MayLandlordName = Convert.ToString(sdr["MayLandlordName"]);
                                form80CHeader.JuneLandlordName = Convert.ToString(sdr["JuneLandlordName"]);
                                form80CHeader.JulyLandlordName = Convert.ToString(sdr["JulyLandlordName"]);
                                form80CHeader.AugustLandlordName = Convert.ToString(sdr["AugustLandlordName"]);
                                form80CHeader.SeptemberLandlordName = Convert.ToString(sdr["SeptemberLandlordName"]);
                                form80CHeader.OctoberLandlordName = Convert.ToString(sdr["OctoberLandlordName"]);
                                form80CHeader.NovemberLandlordName = Convert.ToString(sdr["NovemberLandlordName"]);
                                form80CHeader.DecemberLandlordName = Convert.ToString(sdr["DecemberLandlordName"]);
                                form80CHeader.JanuaryLandlordName = Convert.ToString(sdr["JanuaryLandlordName"]);
                                form80CHeader.FebruaryLandlordName = Convert.ToString(sdr["FebruaryLandlordName"]);
                                form80CHeader.MarchLandlordName = Convert.ToString(sdr["MarchLandlordName"]);
                                form80CHeader.AprilLandlordAddress = Convert.ToString(sdr["AprilLandlordAddress"]);
                                form80CHeader.MayLandlordAddress = Convert.ToString(sdr["MayLandlordAddress"]);
                                form80CHeader.JuneLandlordAddress = Convert.ToString(sdr["JuneLandlordAddress"]);
                                form80CHeader.JulyLandlordAddress = Convert.ToString(sdr["JulyLandlordAddress"]);
                                form80CHeader.AugustLandlordAddress = Convert.ToString(sdr["AugustLandlordAddress"]);
                                form80CHeader.SeptemberLandlordAddress = Convert.ToString(sdr["SeptemberLandlordAddress"]);
                                form80CHeader.OctoberLandlordAddress = Convert.ToString(sdr["OctoberLandlordAddress"]);
                                form80CHeader.NovemberLandlordAddress = Convert.ToString(sdr["NovemberLandlordAddress"]);
                                form80CHeader.DecemberLandlordAddress = Convert.ToString(sdr["DecemberLandlordAddress"]);
                                form80CHeader.JanuaryLandlordAddress = Convert.ToString(sdr["JanuaryLandlordAddress"]);
                                form80CHeader.FebruaryLandlordAddress = Convert.ToString(sdr["FebruaryLandlordAddress"]);
                                form80CHeader.MarchLandlordAddress = Convert.ToString(sdr["MarchLandlordAddress"]);
                                form80CHeader.RentProofFile = Convert.ToString(sdr["RentProofFile"]);

                            }
                        }
                        catch
                        {
                            return View();
                        }
                    }
                }

            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deductionLine_get"))
                {
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80C form80C = new Form80C();
                            _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();

                            _TaxDeclFormSetting.Id = Convert.ToInt32(sdr["settingsid"]);
                            form80C.Form80CsettingId = Convert.ToInt32(sdr["settingsid"]);

                            form80C.Id = Convert.ToInt32(sdr["id"]);

                            _TaxDeclFormSetting.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);
                            form80C.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);

                            _TaxDeclFormSetting.Description = Convert.ToString(sdr["Description"]);
                            form80C.DescriptionLabel = Convert.ToString(sdr["Description"]);

                            _TaxDeclFormSetting.ControlType = Convert.ToInt32(sdr["ControlType"]);
                            //form80C.ControlType = Convert.ToInt32(sdr["ControlType"]);

                            _TaxDeclFormSetting.LineNumber = Convert.ToString(sdr["LineNumber"]);
                            form80C.LineNumber = Convert.ToString(sdr["LineNumber"]);

                            _TaxDeclFormSetting.Section = Convert.ToString(sdr["sectiondescription"]);
                            form80C.Section = Convert.ToString(sdr["sectiondescription"]);

                            _TaxDeclFormSetting.SectionName = Convert.ToString(sdr["SectionName"]);
                            form80C.SectionName = Convert.ToString(sdr["SectionName"]);
                            form80C.Declared_Amt = Convert.ToDecimal(sdr["Declared_Amt"]);
                            form80C.Proof_Amt = Convert.ToDecimal(sdr["Proof_Amt"]);
                            form80C.Proof_file_location = Convert.ToString(sdr["Proof_file"]);

                            form80C.Description = Convert.ToString(sdr["Value"]);


                            _TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                            //form80C.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);

                            _TaxDeclFormSetting.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);
                            //form80C.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);

                            _TaxDeclFormSetting.AttachmentType = Convert.ToString(sdr["AttachmentType"]);
                            //form80C.AttachmentType = Convert.ToString(sdr["AttachmentType"]);

                            _TaxDeclFormSetting.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);
                            //form80C.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);

                            //_TaxDeclFormSettings.Add(_TaxDeclFormSetting);
                            form80CHeader.Form80CList.Add(form80C);
                        }
                    }
                    con.Close();
                }
            }

            if (form80CHeader.Form80CList.Count == 0)
            {
                form80CHeader.Form80CList.Add(new Form80C());
            }

            ViewBag.headerid = form80CHeader.headerid;

            return View(form80CHeader);
        }

        // POST: Home/UpdateForm80Cdeduction/5
         [HandleError]
        [HttpPost]

        public ActionResult UpdateForm80Cdeduction(Form80CHeader form80CHeader)
        {
            string rent_proof = SaveToPhysicalLocation(form80CHeader.RentProofFileBase, form80CHeader.Empid, DateTime.Now);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormHeader_update", con))
                {
                    //form80CHeader.headerid = ViewBag.headerid;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", form80CHeader.headerid);
                    cmd.Parameters.AddWithValue("@Empid", form80CHeader.Empid);
                    cmd.Parameters.AddWithValue("@Employeeid", form80CHeader.Employeeid);
                    cmd.Parameters.AddWithValue("@AprilMetro", form80CHeader.AprilMetro);
                    cmd.Parameters.AddWithValue("@MayMetro", form80CHeader.MayMetro);
                    cmd.Parameters.AddWithValue("@JuneMetro", form80CHeader.JuneMetro);
                    cmd.Parameters.AddWithValue("@JulyMetro", form80CHeader.JulyMetro);
                    cmd.Parameters.AddWithValue("@AugustMetro", form80CHeader.AugustMetro);
                    cmd.Parameters.AddWithValue("@SeptemberMetro", form80CHeader.SeptemberMetro);
                    cmd.Parameters.AddWithValue("@OctoberMetro", form80CHeader.OctoberMetro);
                    cmd.Parameters.AddWithValue("@NovemberMetro", form80CHeader.NovemberMetro);
                    cmd.Parameters.AddWithValue("@DecemberMetro", form80CHeader.DecemberMetro);
                    cmd.Parameters.AddWithValue("@JanuaryMetro", form80CHeader.JanuaryMetro);
                    cmd.Parameters.AddWithValue("@FebruaryMetro", form80CHeader.FebruaryMetro);
                    cmd.Parameters.AddWithValue("@MarchMetro", form80CHeader.MarchMetro);
                    cmd.Parameters.AddWithValue("@AprilProofSubmission", form80CHeader.AprilProofSubmission);
                    cmd.Parameters.AddWithValue("@MayProofSubmission", form80CHeader.MayProofSubmission);
                    cmd.Parameters.AddWithValue("@JuneProofSubmission", form80CHeader.JuneProofSubmission);
                    cmd.Parameters.AddWithValue("@JulyProofSubmission", form80CHeader.JulyProofSubmission);
                    cmd.Parameters.AddWithValue("@AugustProofSubmission", form80CHeader.AugustProofSubmission);
                    cmd.Parameters.AddWithValue("@SeptemberProofSubmission", form80CHeader.SeptemberProofSubmission);
                    cmd.Parameters.AddWithValue("@OctoberProofSubmission", form80CHeader.OctoberProofSubmission);
                    cmd.Parameters.AddWithValue("@NovemberProofSubmission", form80CHeader.NovemberProofSubmission);
                    cmd.Parameters.AddWithValue("@DecemberProofSubmission", form80CHeader.DecemberProofSubmission);
                    cmd.Parameters.AddWithValue("@JanuaryProofSubmission", form80CHeader.JanuaryProofSubmission);
                    cmd.Parameters.AddWithValue("@FebruaryProofSubmission", form80CHeader.FebruaryProofSubmission);
                    cmd.Parameters.AddWithValue("@MarchProofSubmission", form80CHeader.MarchProofSubmission);
                    cmd.Parameters.AddWithValue("@AprilNonMetro", form80CHeader.AprilNonMetro);
                    cmd.Parameters.AddWithValue("@MayNonMetro", form80CHeader.MayNonMetro);
                    cmd.Parameters.AddWithValue("@JuneNonMetro", form80CHeader.JuneNonMetro);
                    cmd.Parameters.AddWithValue("@JulyNonMetro", form80CHeader.JulyNonMetro);
                    cmd.Parameters.AddWithValue("@AugustNonMetro", form80CHeader.AugustNonMetro);
                    cmd.Parameters.AddWithValue("@SeptemberNonMetro", form80CHeader.SeptemberNonMetro);
                    cmd.Parameters.AddWithValue("@OctoberNonMetro", form80CHeader.OctoberNonMetro);
                    cmd.Parameters.AddWithValue("@NovemberNonMetro", form80CHeader.NovemberNonMetro);
                    cmd.Parameters.AddWithValue("@DecemberNonMetro", form80CHeader.DecemberNonMetro);
                    cmd.Parameters.AddWithValue("@JanuaryNonMetro", form80CHeader.JanuaryNonMetro);
                    cmd.Parameters.AddWithValue("@FebruaryNonMetro", form80CHeader.FebruaryNonMetro);
                    cmd.Parameters.AddWithValue("@MarchNonMetro", form80CHeader.MarchNonMetro);
                    cmd.Parameters.AddWithValue("@AprilNonMetroProofSubmission", form80CHeader.AprilNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@MayNonMetroProofSubmission", form80CHeader.MayNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@JuneNonMetroProofSubmission", form80CHeader.JuneNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@JulyNonMetroProofSubmission", form80CHeader.JulyNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@AugustNonMetroProofSubmission", form80CHeader.AugustNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@SeptemberNonMetroProofSubmission", form80CHeader.SeptemberNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@OctoberNonMetroProofSubmission", form80CHeader.OctoberNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@NovemberNonMetroProofSubmission", form80CHeader.NovemberNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@DecemberNonMetroProofSubmission", form80CHeader.DecemberNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@JanuaryNonMetroProofSubmission", form80CHeader.JanuaryNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@FebruaryNonMetroProofSubmission", form80CHeader.FebruaryNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@MarchNonMetroProofSubmission", form80CHeader.MarchNonMetroProofSubmission);
                    cmd.Parameters.AddWithValue("@AprilRemarks", form80CHeader.AprilRemarks);
                    cmd.Parameters.AddWithValue("@MayRemarks", form80CHeader.MayRemarks);
                    cmd.Parameters.AddWithValue("@JuneRemarks", form80CHeader.JuneRemarks);
                    cmd.Parameters.AddWithValue("@JulyRemarks", form80CHeader.JulyRemarks);
                    cmd.Parameters.AddWithValue("@AugustRemarks", form80CHeader.AugustRemarks);
                    cmd.Parameters.AddWithValue("@SeptemberRemarks", form80CHeader.SeptemberRemarks);
                    cmd.Parameters.AddWithValue("@OctoberRemarks", form80CHeader.OctoberRemarks);
                    cmd.Parameters.AddWithValue("@NovemberRemarks", form80CHeader.NovemberRemarks);
                    cmd.Parameters.AddWithValue("@DecemberRemarks", form80CHeader.DecemberRemarks);
                    cmd.Parameters.AddWithValue("@JanuaryRemarks", form80CHeader.JanuaryRemarks);
                    cmd.Parameters.AddWithValue("@FebruaryRemarks", form80CHeader.FebruaryRemarks);
                    cmd.Parameters.AddWithValue("@MarchRemarks", form80CHeader.MarchRemarks);
                    cmd.Parameters.AddWithValue("@AprilLandlordPAN", form80CHeader.AprilLandlordPAN);
                    cmd.Parameters.AddWithValue("@MayLandlordPAN", form80CHeader.MayLandlordPAN);
                    cmd.Parameters.AddWithValue("@JuneLandlordPAN", form80CHeader.JuneLandlordPAN);
                    cmd.Parameters.AddWithValue("@JulyLandlordPAN", form80CHeader.JulyLandlordPAN);
                    cmd.Parameters.AddWithValue("@AugustLandlordPAN", form80CHeader.AugustLandlordPAN);
                    cmd.Parameters.AddWithValue("@SeptemberLandlordPAN", form80CHeader.SeptemberLandlordPAN);
                    cmd.Parameters.AddWithValue("@OctoberLandlordPAN", form80CHeader.OctoberLandlordPAN);
                    cmd.Parameters.AddWithValue("@NovemberLandlordPAN", form80CHeader.NovemberLandlordPAN);
                    cmd.Parameters.AddWithValue("@DecemberLandlordPAN", form80CHeader.DecemberLandlordPAN);
                    cmd.Parameters.AddWithValue("@JanuaryLandlordPAN", form80CHeader.JanuaryLandlordPAN);
                    cmd.Parameters.AddWithValue("@FebruaryLandlordPAN", form80CHeader.FebruaryLandlordPAN);
                    cmd.Parameters.AddWithValue("@MarchLandlordPAN", form80CHeader.MarchLandlordPAN);
                    cmd.Parameters.AddWithValue("@AprilLandlordName", form80CHeader.AprilLandlordName);
                    cmd.Parameters.AddWithValue("@MayLandlordName", form80CHeader.MayLandlordName);
                    cmd.Parameters.AddWithValue("@JuneLandlordName", form80CHeader.JuneLandlordName);
                    cmd.Parameters.AddWithValue("@JulyLandlordName", form80CHeader.JulyLandlordName);
                    cmd.Parameters.AddWithValue("@AugustLandlordName", form80CHeader.AugustLandlordName);
                    cmd.Parameters.AddWithValue("@SeptemberLandlordName", form80CHeader.SeptemberLandlordName);
                    cmd.Parameters.AddWithValue("@OctoberLandlordName", form80CHeader.OctoberLandlordName);
                    cmd.Parameters.AddWithValue("@NovemberLandlordName", form80CHeader.NovemberLandlordName);
                    cmd.Parameters.AddWithValue("@DecemberLandlordName", form80CHeader.DecemberLandlordName);
                    cmd.Parameters.AddWithValue("@JanuaryLandlordName", form80CHeader.JanuaryLandlordName);
                    cmd.Parameters.AddWithValue("@FebruaryLandlordName", form80CHeader.FebruaryLandlordName);
                    cmd.Parameters.AddWithValue("@MarchLandlordName", form80CHeader.MarchLandlordName);
                    cmd.Parameters.AddWithValue("@AprilLandlordAddress", form80CHeader.AprilLandlordAddress);
                    cmd.Parameters.AddWithValue("@MayLandlordAddress", form80CHeader.MayLandlordAddress);
                    cmd.Parameters.AddWithValue("@JuneLandlordAddress", form80CHeader.JuneLandlordAddress);
                    cmd.Parameters.AddWithValue("@JulyLandlordAddress", form80CHeader.JulyLandlordAddress);
                    cmd.Parameters.AddWithValue("@AugustLandlordAddress", form80CHeader.AugustLandlordAddress);
                    cmd.Parameters.AddWithValue("@SeptemberLandlordAddress", form80CHeader.SeptemberLandlordAddress);
                    cmd.Parameters.AddWithValue("@OctoberLandlordAddress", form80CHeader.OctoberLandlordAddress);
                    cmd.Parameters.AddWithValue("@NovemberLandlordAddress", form80CHeader.NovemberLandlordAddress);
                    cmd.Parameters.AddWithValue("@DecemberLandlordAddress", form80CHeader.DecemberLandlordAddress);
                    cmd.Parameters.AddWithValue("@JanuaryLandlordAddress", form80CHeader.JanuaryLandlordAddress);
                    cmd.Parameters.AddWithValue("@FebruaryLandlordAddress", form80CHeader.FebruaryLandlordAddress);
                    cmd.Parameters.AddWithValue("@MarchLandlordAddress", form80CHeader.MarchLandlordAddress);
                    cmd.Parameters.AddWithValue("@RentProofFile", rent_proof);
                    cmd.Parameters.AddWithValue("@monthyear", form80CHeader.MonthYear);

                    con.Open();
                    Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();
                    con.Close();
                }


                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_update", con))
                {
                    foreach (Form80C form80C in form80CHeader.Form80CList)
                    {
                        string Proof_file = SaveToPhysicalLocation(form80C.Proof_file, form80CHeader.Empid, DateTime.Now);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", form80C.Id);
                        cmd.Parameters.AddWithValue("@TaxDeclFormid", form80C.TaxId);
                        cmd.Parameters.AddWithValue("@TaxDeclFormSettingid", form80C.Form80CsettingId);
                        cmd.Parameters.AddWithValue("@Value", form80C.Description != null ? form80C.Description : form80C.Descriptioncheckbox.ToString());
                        cmd.Parameters.AddWithValue("@Declared_Amt", form80C.Declared_Amt);
                        cmd.Parameters.AddWithValue("@Proof_Amt", form80C.Proof_Amt);
                        //if (Proof_file == "")
                        //{
                        //    Proof_file = form80C.Proof_file_location;
                        //}                        
                        cmd.Parameters.AddWithValue("@Proof_file", Proof_file);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@EmployeeId", form80CHeader.Empid);
                        cmd.Parameters.AddWithValue("@monthyear", form80CHeader.MonthYear);
                        cmd.Parameters.AddWithValue("@UserId", "");
                        cmd.Parameters.AddWithValue("@Form80CHeaderid", form80CHeader.headerid);
                        con.Open();
                        int id = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        con.Close();
                    }

                }
            }
            //return View(_TaxDeclFormSetting);
            return RedirectToAction("Details", "Employee");
        }
        [HandleError]
        public ActionResult UpdateForm(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form80C form80C = new Form80C();
            string query = "sp_TaxDeclFormEntry_get";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@taxdeclentryid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80C form80c = new Form80C
                            {

                                TaxId = Convert.ToInt32(sdr["TaxDeclFormid"]),
                                Form80CsettingId = Convert.ToInt32(sdr["Form80CsettingId"]),
                                Declared_Amt = Convert.ToInt32(sdr["Declared_Amt"]),
                                Proof_Amt = Convert.ToInt32(sdr["Proof_Amt"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                Id = Convert.ToInt32(sdr["Id"])

                            };
                        }
                    }
                    con.Close();
                }
            }
            if (form80C == null)
            {
                return HttpNotFound();
            }
            return View(form80C);
        }

        // 3. *************Delete Formdeductions ******************

        // GET: Home/DeleteFormdeductions/5
        [HandleError]
        public ActionResult DeleteFormdeductions(int? id, DateTime year)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form80CHeader form80CHeader = new Form80CHeader();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EmployeeTaxDeclEntry_get"))
                {
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80CHeader.Empid = Convert.ToInt32(id);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.AprilMetro = Convert.ToString(sdr["AprilMetro"]);
                            form80CHeader.headerid = Convert.ToInt32(sdr["headerid"]);
                            form80CHeader.MayMetro = Convert.ToString(sdr["MayMetro"]);
                            form80CHeader.JuneMetro = Convert.ToString(sdr["JuneMetro"]);
                            form80CHeader.JulyMetro = Convert.ToString(sdr["JulyMetro"]);
                            form80CHeader.AugustMetro = Convert.ToString(sdr["AugustMetro"]);
                            form80CHeader.SeptemberMetro = Convert.ToString(sdr["SeptemberMetro"]);
                            form80CHeader.OctoberMetro = Convert.ToString(sdr["OctoberMetro"]);
                            form80CHeader.NovemberMetro = Convert.ToString(sdr["NovemberMetro"]);
                            form80CHeader.DecemberMetro = Convert.ToString(sdr["DecemberMetro"]);
                            form80CHeader.JanuaryMetro = Convert.ToString(sdr["JanuaryMetro"]);
                            form80CHeader.FebruaryMetro = Convert.ToString(sdr["FebruaryMetro"]);
                            form80CHeader.MarchMetro = Convert.ToString(sdr["MarchMetro"]);
                            form80CHeader.AprilProofSubmission = Convert.ToString(sdr["AprilProofSubmission"]);
                            form80CHeader.MayProofSubmission = Convert.ToString(sdr["MayProofSubmission"]);
                            form80CHeader.JuneProofSubmission = Convert.ToString(sdr["JuneProofSubmission"]);
                            form80CHeader.JulyProofSubmission = Convert.ToString(sdr["JulyProofSubmission"]);
                            form80CHeader.AugustProofSubmission = Convert.ToString(sdr["AugustProofSubmission"]);
                            form80CHeader.SeptemberProofSubmission = Convert.ToString(sdr["SeptemberProofSubmission"]);
                            form80CHeader.OctoberProofSubmission = Convert.ToString(sdr["OctoberProofSubmission"]);
                            form80CHeader.NovemberProofSubmission = Convert.ToString(sdr["NovemberProofSubmission"]);
                            form80CHeader.DecemberProofSubmission = Convert.ToString(sdr["DecemberProofSubmission"]);
                            form80CHeader.JanuaryProofSubmission = Convert.ToString(sdr["JanuaryProofSubmission"]);
                            form80CHeader.FebruaryProofSubmission = Convert.ToString(sdr["FebruaryProofSubmission"]);
                            form80CHeader.MarchProofSubmission = Convert.ToString(sdr["MarchProofSubmission"]);
                            form80CHeader.AprilNonMetro = Convert.ToString(sdr["AprilNonMetro"]);
                            form80CHeader.MayNonMetro = Convert.ToString(sdr["MayNonMetro"]);
                            form80CHeader.JuneNonMetro = Convert.ToString(sdr["JuneNonMetro"]);
                            form80CHeader.JulyNonMetro = Convert.ToString(sdr["JulyNonMetro"]);
                            form80CHeader.AugustNonMetro = Convert.ToString(sdr["AugustNonMetro"]);
                            form80CHeader.SeptemberNonMetro = Convert.ToString(sdr["SeptemberNonMetro"]);
                            form80CHeader.OctoberNonMetro = Convert.ToString(sdr["OctoberNonMetro"]);
                            form80CHeader.NovemberNonMetro = Convert.ToString(sdr["NovemberNonMetro"]);
                            form80CHeader.DecemberNonMetro = Convert.ToString(sdr["DecemberNonMetro"]);
                            form80CHeader.JanuaryNonMetro = Convert.ToString(sdr["JanuaryNonMetro"]);
                            form80CHeader.FebruaryNonMetro = Convert.ToString(sdr["FebruaryNonMetro"]);
                            form80CHeader.MarchNonMetro = Convert.ToString(sdr["MarchNonMetro"]);
                            form80CHeader.AprilNonMetroProofSubmission = Convert.ToString(sdr["AprilNonMetroProofSubmission"]);
                            form80CHeader.MayNonMetroProofSubmission = Convert.ToString(sdr["MayNonMetroProofSubmission"]);
                            form80CHeader.JuneNonMetroProofSubmission = Convert.ToString(sdr["JuneNonMetroProofSubmission"]);
                            form80CHeader.JulyNonMetroProofSubmission = Convert.ToString(sdr["JulyNonMetroProofSubmission"]);
                            form80CHeader.AugustNonMetroProofSubmission = Convert.ToString(sdr["AugustNonMetroProofSubmission"]);
                            form80CHeader.SeptemberNonMetroProofSubmission = Convert.ToString(sdr["SeptemberNonMetroProofSubmission"]);
                            form80CHeader.OctoberNonMetroProofSubmission = Convert.ToString(sdr["OctoberNonMetroProofSubmission"]);
                            form80CHeader.NovemberNonMetroProofSubmission = Convert.ToString(sdr["NovemberNonMetroProofSubmission"]);
                            form80CHeader.DecemberNonMetroProofSubmission = Convert.ToString(sdr["DecemberNonMetroProofSubmission"]);
                            form80CHeader.JanuaryNonMetroProofSubmission = Convert.ToString(sdr["JanuaryNonMetroProofSubmission"]);
                            form80CHeader.FebruaryNonMetroProofSubmission = Convert.ToString(sdr["FebruaryNonMetroProofSubmission"]);
                            form80CHeader.MarchNonMetroProofSubmission = Convert.ToString(sdr["MarchNonMetroProofSubmission"]);
                            form80CHeader.AprilRemarks = Convert.ToString(sdr["AprilRemarks"]);
                            form80CHeader.MayRemarks = Convert.ToString(sdr["MayRemarks"]);
                            form80CHeader.JuneRemarks = Convert.ToString(sdr["JuneRemarks"]);
                            form80CHeader.JulyRemarks = Convert.ToString(sdr["JulyRemarks"]);
                            form80CHeader.AugustRemarks = Convert.ToString(sdr["AugustRemarks"]);
                            form80CHeader.SeptemberRemarks = Convert.ToString(sdr["SeptemberRemarks"]);
                            form80CHeader.OctoberRemarks = Convert.ToString(sdr["OctoberRemarks"]);
                            form80CHeader.NovemberRemarks = Convert.ToString(sdr["NovemberRemarks"]);
                            form80CHeader.DecemberRemarks = Convert.ToString(sdr["DecemberRemarks"]);
                            form80CHeader.JanuaryRemarks = Convert.ToString(sdr["JanuaryRemarks"]);
                            form80CHeader.FebruaryRemarks = Convert.ToString(sdr["FebruaryRemarks"]);
                            form80CHeader.MarchRemarks = Convert.ToString(sdr["MarchRemarks"]);
                            form80CHeader.AprilLandlordPAN = Convert.ToString(sdr["AprilLandlordPAN"]);
                            form80CHeader.MayLandlordPAN = Convert.ToString(sdr["MayLandlordPAN"]);
                            form80CHeader.JuneLandlordPAN = Convert.ToString(sdr["JuneLandlordPAN"]);
                            form80CHeader.JulyLandlordPAN = Convert.ToString(sdr["JulyLandlordPAN"]);
                            form80CHeader.AugustLandlordPAN = Convert.ToString(sdr["AugustLandlordPAN"]);
                            form80CHeader.SeptemberLandlordPAN = Convert.ToString(sdr["SeptemberLandlordPAN"]);
                            form80CHeader.OctoberLandlordPAN = Convert.ToString(sdr["OctoberLandlordPAN"]);
                            form80CHeader.NovemberLandlordPAN = Convert.ToString(sdr["NovemberLandlordPAN"]);
                            form80CHeader.DecemberLandlordPAN = Convert.ToString(sdr["DecemberLandlordPAN"]);
                            form80CHeader.JanuaryLandlordPAN = Convert.ToString(sdr["JanuaryLandlordPAN"]);
                            form80CHeader.FebruaryLandlordPAN = Convert.ToString(sdr["FebruaryLandlordPAN"]);
                            form80CHeader.MarchLandlordPAN = Convert.ToString(sdr["MarchLandlordPAN"]);
                            form80CHeader.AprilLandlordName = Convert.ToString(sdr["AprilLandlordName"]);
                            form80CHeader.MayLandlordName = Convert.ToString(sdr["MayLandlordName"]);
                            form80CHeader.JuneLandlordName = Convert.ToString(sdr["JuneLandlordName"]);
                            form80CHeader.JulyLandlordName = Convert.ToString(sdr["JulyLandlordName"]);
                            form80CHeader.AugustLandlordName = Convert.ToString(sdr["AugustLandlordName"]);
                            form80CHeader.SeptemberLandlordName = Convert.ToString(sdr["SeptemberLandlordName"]);
                            form80CHeader.OctoberLandlordName = Convert.ToString(sdr["OctoberLandlordName"]);
                            form80CHeader.NovemberLandlordName = Convert.ToString(sdr["NovemberLandlordName"]);
                            form80CHeader.DecemberLandlordName = Convert.ToString(sdr["DecemberLandlordName"]);
                            form80CHeader.JanuaryLandlordName = Convert.ToString(sdr["JanuaryLandlordName"]);
                            form80CHeader.FebruaryLandlordName = Convert.ToString(sdr["FebruaryLandlordName"]);
                            form80CHeader.MarchLandlordName = Convert.ToString(sdr["MarchLandlordName"]);
                            form80CHeader.AprilLandlordAddress = Convert.ToString(sdr["AprilLandlordAddress"]);
                            form80CHeader.MayLandlordAddress = Convert.ToString(sdr["MayLandlordAddress"]);
                            form80CHeader.JuneLandlordAddress = Convert.ToString(sdr["JuneLandlordAddress"]);
                            form80CHeader.JulyLandlordAddress = Convert.ToString(sdr["JulyLandlordAddress"]);
                            form80CHeader.AugustLandlordAddress = Convert.ToString(sdr["AugustLandlordAddress"]);
                            form80CHeader.SeptemberLandlordAddress = Convert.ToString(sdr["SeptemberLandlordAddress"]);
                            form80CHeader.OctoberLandlordAddress = Convert.ToString(sdr["OctoberLandlordAddress"]);
                            form80CHeader.NovemberLandlordAddress = Convert.ToString(sdr["NovemberLandlordAddress"]);
                            form80CHeader.DecemberLandlordAddress = Convert.ToString(sdr["DecemberLandlordAddress"]);
                            form80CHeader.JanuaryLandlordAddress = Convert.ToString(sdr["JanuaryLandlordAddress"]);
                            form80CHeader.FebruaryLandlordAddress = Convert.ToString(sdr["FebruaryLandlordAddress"]);
                            form80CHeader.MarchLandlordAddress = Convert.ToString(sdr["MarchLandlordAddress"]);
                            form80CHeader.RentProofFile = Convert.ToString(sdr["RentProofFile"]);

                        };
                    }
                }
                con.Close();


                using (SqlCommand cmd = new SqlCommand("sp_EmployeeTaxDeclEntryLine_get"))
                {
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Form80C form80C = new Form80C();
                            _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();

                            _TaxDeclFormSetting.Id = Convert.ToInt32(sdr["settingsid"]);
                            form80C.Form80CsettingId = Convert.ToInt32(sdr["settingsid"]);

                            _TaxDeclFormSetting.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);
                            form80C.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);
                            form80C.Id = Convert.ToInt32(sdr["id"]);

                            _TaxDeclFormSetting.Description = Convert.ToString(sdr["Description"]);
                            form80C.DescriptionLabel = Convert.ToString(sdr["Description"]);

                            _TaxDeclFormSetting.ControlType = Convert.ToInt32(sdr["ControlType"]);
                            //form80C.ControlType = Convert.ToInt32(sdr["ControlType"]);

                            _TaxDeclFormSetting.LineNumber = Convert.ToString(sdr["LineNumber"]);
                            form80C.LineNumber = Convert.ToString(sdr["LineNumber"]);

                            _TaxDeclFormSetting.Section = Convert.ToString(sdr["sectiondescription"]);
                            form80C.Section = Convert.ToString(sdr["sectiondescription"]);

                            _TaxDeclFormSetting.SectionName = Convert.ToString(sdr["SectionName"]);
                            form80C.SectionName = Convert.ToString(sdr["SectionName"]);
                            form80C.Declared_Amt = Convert.ToDecimal(sdr["Declared_Amt"]);
                            form80C.Proof_Amt = Convert.ToDecimal(sdr["Proof_Amt"]);
                            form80C.Proof_file_location = Convert.ToString(sdr["Proof_file"]);
                            form80C.Description = Convert.ToString(sdr["Value"]);


                            _TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                            //form80C.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);

                            _TaxDeclFormSetting.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);
                            //form80C.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);

                            _TaxDeclFormSetting.AttachmentType = Convert.ToString(sdr["AttachmentType"]);
                            //form80C.AttachmentType = Convert.ToString(sdr["AttachmentType"]);

                            _TaxDeclFormSetting.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);
                            //form80C.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);

                            //_TaxDeclFormSettings.Add(_TaxDeclFormSetting);
                            form80CHeader.Form80CList.Add(form80C);
                        }
                    }
                    con.Close();
                }
            }

            if (form80CHeader.Form80CList.Count == 0)
            {
                form80CHeader.Form80CList.Add(new Form80C());
            }


            return View(form80CHeader);
        }


        // POST: Home/DeleteFormdeductions/5
        [HandleError]
        [HttpPost, ActionName("DeleteFormdeductions")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteFormdeductions(Form80CHeader form80CHeader)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {


                using (SqlCommand cmd = new SqlCommand("so_TaxDecl_delete", con))
                {
                    cmd.Parameters.AddWithValue("@TaxDeclFormHeaderid", form80CHeader.headerid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return RedirectToAction("Details", "Employee");
        }

        //show Formdeductiondetails
        [HandleError]
        [HttpGet]
        public ActionResult Formdeductiondetails(int id, DateTime year)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Form80CHeader form80CHeader = new Form80CHeader();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deductionHeader_get"))
                {
                    cmd.Parameters.AddWithValue("@emplid", id);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80CHeader.CreatedDate = year;

                            form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.AprilMetro = Convert.ToString(sdr["AprilMetro"]);
                            form80CHeader.MayMetro = Convert.ToString(sdr["MayMetro"]);
                            form80CHeader.JuneMetro = Convert.ToString(sdr["JuneMetro"]);
                            form80CHeader.JulyMetro = Convert.ToString(sdr["JulyMetro"]);
                            form80CHeader.AugustMetro = Convert.ToString(sdr["AugustMetro"]);
                            form80CHeader.SeptemberMetro = Convert.ToString(sdr["SeptemberMetro"]);
                            form80CHeader.OctoberMetro = Convert.ToString(sdr["OctoberMetro"]);
                            form80CHeader.NovemberMetro = Convert.ToString(sdr["NovemberMetro"]);
                            form80CHeader.DecemberMetro = Convert.ToString(sdr["DecemberMetro"]);
                            form80CHeader.JanuaryMetro = Convert.ToString(sdr["JanuaryMetro"]);
                            form80CHeader.FebruaryMetro = Convert.ToString(sdr["FebruaryMetro"]);
                            form80CHeader.MarchMetro = Convert.ToString(sdr["MarchMetro"]);
                            form80CHeader.AprilProofSubmission = Convert.ToString(sdr["AprilProofSubmission"]);
                            form80CHeader.MayProofSubmission = Convert.ToString(sdr["MayProofSubmission"]);
                            form80CHeader.JuneProofSubmission = Convert.ToString(sdr["JuneProofSubmission"]);
                            form80CHeader.JulyProofSubmission = Convert.ToString(sdr["JulyProofSubmission"]);
                            form80CHeader.AugustProofSubmission = Convert.ToString(sdr["AugustProofSubmission"]);
                            form80CHeader.SeptemberProofSubmission = Convert.ToString(sdr["SeptemberProofSubmission"]);
                            form80CHeader.OctoberProofSubmission = Convert.ToString(sdr["OctoberProofSubmission"]);
                            form80CHeader.NovemberProofSubmission = Convert.ToString(sdr["NovemberProofSubmission"]);
                            form80CHeader.DecemberProofSubmission = Convert.ToString(sdr["DecemberProofSubmission"]);
                            form80CHeader.JanuaryProofSubmission = Convert.ToString(sdr["JanuaryProofSubmission"]);
                            form80CHeader.FebruaryProofSubmission = Convert.ToString(sdr["FebruaryProofSubmission"]);
                            form80CHeader.MarchProofSubmission = Convert.ToString(sdr["MarchProofSubmission"]);
                            form80CHeader.AprilNonMetro = Convert.ToString(sdr["AprilNonMetro"]);
                            form80CHeader.MayNonMetro = Convert.ToString(sdr["MayNonMetro"]);
                            form80CHeader.JuneNonMetro = Convert.ToString(sdr["JuneNonMetro"]);
                            form80CHeader.JulyNonMetro = Convert.ToString(sdr["JulyNonMetro"]);
                            form80CHeader.AugustNonMetro = Convert.ToString(sdr["AugustNonMetro"]);
                            form80CHeader.SeptemberNonMetro = Convert.ToString(sdr["SeptemberNonMetro"]);
                            form80CHeader.OctoberNonMetro = Convert.ToString(sdr["OctoberNonMetro"]);
                            form80CHeader.NovemberNonMetro = Convert.ToString(sdr["NovemberNonMetro"]);
                            form80CHeader.DecemberNonMetro = Convert.ToString(sdr["DecemberNonMetro"]);
                            form80CHeader.JanuaryNonMetro = Convert.ToString(sdr["JanuaryNonMetro"]);
                            form80CHeader.FebruaryNonMetro = Convert.ToString(sdr["FebruaryNonMetro"]);
                            form80CHeader.MarchNonMetro = Convert.ToString(sdr["MarchNonMetro"]);
                            form80CHeader.AprilNonMetroProofSubmission = Convert.ToString(sdr["AprilNonMetroProofSubmission"]);
                            form80CHeader.MayNonMetroProofSubmission = Convert.ToString(sdr["MayNonMetroProofSubmission"]);
                            form80CHeader.JuneNonMetroProofSubmission = Convert.ToString(sdr["JuneNonMetroProofSubmission"]);
                            form80CHeader.JulyNonMetroProofSubmission = Convert.ToString(sdr["JulyNonMetroProofSubmission"]);
                            form80CHeader.AugustNonMetroProofSubmission = Convert.ToString(sdr["AugustNonMetroProofSubmission"]);
                            form80CHeader.SeptemberNonMetroProofSubmission = Convert.ToString(sdr["SeptemberNonMetroProofSubmission"]);
                            form80CHeader.OctoberNonMetroProofSubmission = Convert.ToString(sdr["OctoberNonMetroProofSubmission"]);
                            form80CHeader.NovemberNonMetroProofSubmission = Convert.ToString(sdr["NovemberNonMetroProofSubmission"]);
                            form80CHeader.DecemberNonMetroProofSubmission = Convert.ToString(sdr["DecemberNonMetroProofSubmission"]);
                            form80CHeader.JanuaryNonMetroProofSubmission = Convert.ToString(sdr["JanuaryNonMetroProofSubmission"]);
                            form80CHeader.FebruaryNonMetroProofSubmission = Convert.ToString(sdr["FebruaryNonMetroProofSubmission"]);
                            form80CHeader.MarchNonMetroProofSubmission = Convert.ToString(sdr["MarchNonMetroProofSubmission"]);
                            form80CHeader.AprilRemarks = Convert.ToString(sdr["AprilRemarks"]);
                            form80CHeader.MayRemarks = Convert.ToString(sdr["MayRemarks"]);
                            form80CHeader.JuneRemarks = Convert.ToString(sdr["JuneRemarks"]);
                            form80CHeader.JulyRemarks = Convert.ToString(sdr["JulyRemarks"]);
                            form80CHeader.AugustRemarks = Convert.ToString(sdr["AugustRemarks"]);
                            form80CHeader.SeptemberRemarks = Convert.ToString(sdr["SeptemberRemarks"]);
                            form80CHeader.OctoberRemarks = Convert.ToString(sdr["OctoberRemarks"]);
                            form80CHeader.NovemberRemarks = Convert.ToString(sdr["NovemberRemarks"]);
                            form80CHeader.DecemberRemarks = Convert.ToString(sdr["DecemberRemarks"]);
                            form80CHeader.JanuaryRemarks = Convert.ToString(sdr["JanuaryRemarks"]);
                            form80CHeader.FebruaryRemarks = Convert.ToString(sdr["FebruaryRemarks"]);
                            form80CHeader.MarchRemarks = Convert.ToString(sdr["MarchRemarks"]);
                            form80CHeader.AprilLandlordPAN = Convert.ToString(sdr["AprilLandlordPAN"]);
                            form80CHeader.MayLandlordPAN = Convert.ToString(sdr["MayLandlordPAN"]);
                            form80CHeader.JuneLandlordPAN = Convert.ToString(sdr["JuneLandlordPAN"]);
                            form80CHeader.JulyLandlordPAN = Convert.ToString(sdr["JulyLandlordPAN"]);
                            form80CHeader.AugustLandlordPAN = Convert.ToString(sdr["AugustLandlordPAN"]);
                            form80CHeader.SeptemberLandlordPAN = Convert.ToString(sdr["SeptemberLandlordPAN"]);
                            form80CHeader.OctoberLandlordPAN = Convert.ToString(sdr["OctoberLandlordPAN"]);
                            form80CHeader.NovemberLandlordPAN = Convert.ToString(sdr["NovemberLandlordPAN"]);
                            form80CHeader.DecemberLandlordPAN = Convert.ToString(sdr["DecemberLandlordPAN"]);
                            form80CHeader.JanuaryLandlordPAN = Convert.ToString(sdr["JanuaryLandlordPAN"]);
                            form80CHeader.FebruaryLandlordPAN = Convert.ToString(sdr["FebruaryLandlordPAN"]);
                            form80CHeader.MarchLandlordPAN = Convert.ToString(sdr["MarchLandlordPAN"]);
                            form80CHeader.AprilLandlordName = Convert.ToString(sdr["AprilLandlordName"]);
                            form80CHeader.MayLandlordName = Convert.ToString(sdr["MayLandlordName"]);
                            form80CHeader.JuneLandlordName = Convert.ToString(sdr["JuneLandlordName"]);
                            form80CHeader.JulyLandlordName = Convert.ToString(sdr["JulyLandlordName"]);
                            form80CHeader.AugustLandlordName = Convert.ToString(sdr["AugustLandlordName"]);
                            form80CHeader.SeptemberLandlordName = Convert.ToString(sdr["SeptemberLandlordName"]);
                            form80CHeader.OctoberLandlordName = Convert.ToString(sdr["OctoberLandlordName"]);
                            form80CHeader.NovemberLandlordName = Convert.ToString(sdr["NovemberLandlordName"]);
                            form80CHeader.DecemberLandlordName = Convert.ToString(sdr["DecemberLandlordName"]);
                            form80CHeader.JanuaryLandlordName = Convert.ToString(sdr["JanuaryLandlordName"]);
                            form80CHeader.FebruaryLandlordName = Convert.ToString(sdr["FebruaryLandlordName"]);
                            form80CHeader.MarchLandlordName = Convert.ToString(sdr["MarchLandlordName"]);
                            form80CHeader.AprilLandlordAddress = Convert.ToString(sdr["AprilLandlordAddress"]);
                            form80CHeader.MayLandlordAddress = Convert.ToString(sdr["MayLandlordAddress"]);
                            form80CHeader.JuneLandlordAddress = Convert.ToString(sdr["JuneLandlordAddress"]);
                            form80CHeader.JulyLandlordAddress = Convert.ToString(sdr["JulyLandlordAddress"]);
                            form80CHeader.AugustLandlordAddress = Convert.ToString(sdr["AugustLandlordAddress"]);
                            form80CHeader.SeptemberLandlordAddress = Convert.ToString(sdr["SeptemberLandlordAddress"]);
                            form80CHeader.OctoberLandlordAddress = Convert.ToString(sdr["OctoberLandlordAddress"]);
                            form80CHeader.NovemberLandlordAddress = Convert.ToString(sdr["NovemberLandlordAddress"]);
                            form80CHeader.DecemberLandlordAddress = Convert.ToString(sdr["DecemberLandlordAddress"]);
                            form80CHeader.JanuaryLandlordAddress = Convert.ToString(sdr["JanuaryLandlordAddress"]);
                            form80CHeader.FebruaryLandlordAddress = Convert.ToString(sdr["FebruaryLandlordAddress"]);
                            form80CHeader.MarchLandlordAddress = Convert.ToString(sdr["MarchLandlordAddress"]);
                            form80CHeader.RentProofFile = Convert.ToString(sdr["RentProofFile"]);
                            form80CHeader.MonthYear = Convert.ToDateTime(sdr["monthyear"]);

                        }
                    }
                    con.Close();


                    using (SqlCommand cmd2 = new SqlCommand("sp_deductionLine2_get"))
                    {
                        cmd2.Parameters.AddWithValue("@emplid", id);
                        cmd2.Parameters.AddWithValue("@year", year);
                        cmd2.Connection = con;
                        cmd2.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader sdr = cmd2.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Form80C form80C = new Form80C();
                                _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();

                                _TaxDeclFormSetting.Id = Convert.ToInt32(sdr["Id"]);
                                form80C.Form80CsettingId = Convert.ToInt32(sdr["Id"]);

                                _TaxDeclFormSetting.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);
                                form80C.SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]);

                                _TaxDeclFormSetting.Description = Convert.ToString(sdr["Description"]);
                                form80C.DescriptionLabel = Convert.ToString(sdr["Description"]);

                                _TaxDeclFormSetting.ControlType = Convert.ToInt32(sdr["ControlType"]);
                                //form80C.ControlType = Convert.ToInt32(sdr["ControlType"]);

                                _TaxDeclFormSetting.LineNumber = Convert.ToString(sdr["LineNumber"]);
                                form80C.LineNumber = Convert.ToString(sdr["LineNumber"]);

                                _TaxDeclFormSetting.Section = Convert.ToString(sdr["sectiondescription"]);
                                form80C.Section = Convert.ToString(sdr["sectiondescription"]);

                                _TaxDeclFormSetting.SectionName = Convert.ToString(sdr["SectionName"]);
                                form80C.SectionName = Convert.ToString(sdr["SectionName"]);
                                form80C.Declared_Amt = Convert.ToDecimal(sdr["Declared_Amt"]);
                                form80C.Proof_Amt = Convert.ToDecimal(sdr["Proof_Amt"]);
                                form80C.Proof_file_location = Convert.ToString(sdr["Proof_file"]);
                                form80C.Description = Convert.ToString(sdr["Value"]);
                                form80C.MonthYear = Convert.ToDateTime(sdr["monthyear"]);

                                _TaxDeclFormSetting.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);
                                //form80C.AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]);

                                _TaxDeclFormSetting.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);
                                //form80C.AttachmentSize = Convert.ToString(sdr["AttachmentSize"]);

                                _TaxDeclFormSetting.AttachmentType = Convert.ToString(sdr["AttachmentType"]);
                                //form80C.AttachmentType = Convert.ToString(sdr["AttachmentType"]);

                                _TaxDeclFormSetting.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);
                                //form80C.MaximumValue = Convert.ToInt32(sdr["MaximumValue"]);

                                //_TaxDeclFormSettings.Add(_TaxDeclFormSetting);
                                form80CHeader.Form80CList.Add(form80C);
                            }
                        }
                        con.Close();
                    }
                }
            }

            if (form80CHeader.Form80CList.Count == 0)
            {
                form80CHeader.Form80CList.Add(new Form80C());
            }
            return View(form80CHeader);
        }





        //show details
        [HandleError]
        [HttpGet]
        public ActionResult FormDetails()
        {
            List<Form80C> form80Cs = new List<Form80C>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_getall"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80Cs.Add(new Form80C
                            {
                                TaxId = Convert.ToInt32(sdr["TaxDeclFormid"]),
                                Form80CsettingId = Convert.ToInt32(sdr["Form80CsettingId"]),
                                Declared_Amt = Convert.ToInt32(sdr["Declared_Amt"]),
                                Proof_Amt = Convert.ToInt32(sdr["Proof_Amt"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                Id = Convert.ToInt32(sdr["Id"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (form80Cs.Count == 0)
            {
                form80Cs.Add(new Form80C());
            }
            return View(form80Cs);
        }

        // GET: Home/FormDetails/5
        // GET: Home/FormDetails/5
        [HandleError]
        public ActionResult FormDetails(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form80C form80C = new Form80C();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_getbyid"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@TaxDeclFormEntryid", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80C = new Form80C
                            {
                                TaxId = Convert.ToInt32(sdr["TaxDeclFormid"]),
                                Form80CsettingId = Convert.ToInt32(sdr["Form80CsettingId"]),
                                Declared_Amt = Convert.ToInt32(sdr["Declared_Amt"]),
                                Proof_Amt = Convert.ToInt32(sdr["Proof_Amt"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                Id = Convert.ToInt32(sdr["Id"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (form80C == null)
            {
                return HttpNotFound();
            }
            return View(form80C);
        }

        // POST: Home/UpdateForm/5        
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateForm(Form80C form80C)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_updatebyid"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@TaxDeclFormid", form80C.TaxId);
                    cmd.Parameters.AddWithValue("@TaxDeclFormSettingid", form80C.Form80CsettingId);
                    cmd.Parameters.AddWithValue("@Value", form80C.Description != null ? form80C.Description : form80C.Descriptioncheckbox.ToString());
                    cmd.Parameters.AddWithValue("@Declared_Amt", form80C.Declared_Amt);
                    cmd.Parameters.AddWithValue("@Proof_Amt", form80C.Proof_Amt);
                    cmd.Parameters.AddWithValue("@MonthYear", form80C.MonthYear);
                    cmd.Parameters.AddWithValue("@EmployeeId", "");
                    cmd.Parameters.AddWithValue("@UserId", "");
                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            // return RedirectToAction("Index");
            //}
            return View(form80C);
        }

        // 3. *************Delete Form ******************

        // GET: Home/DeleteForm/5
        [HandleError]
        public ActionResult DeleteForm(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form80C form80C = new Form80C();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_getbyid"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@TaxDeclFormEntryid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80C = new Form80C
                            {
                                TaxId = Convert.ToInt32(sdr["TaxDeclFormid"]),
                                Form80CsettingId = Convert.ToInt32(sdr["Form80CsettingId"]),
                                Declared_Amt = Convert.ToInt32(sdr["Declared_Amt"]),
                                Proof_Amt = Convert.ToInt32(sdr["Proof_Amt"]),
                                EmployeeId = Convert.ToString(sdr["EmployeeId"]),
                                Id = Convert.ToInt32(sdr["Id"])
                            };

                        };
                    }
                }
                con.Close();
            }
            return View(form80C);
        }

        // POST: Home/DeleteForm/5
        [HandleError]
        [HttpPost, ActionName("DeleteForm")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormEntry_delete", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@TaxDeclFormEntryid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("FormDetails");
        }

        //[HttpPost]
        //public ActionResult Form80C(FormTaxDecl Form80C)
        //{
        //    return View(form80C);
        //}

        // 2. *************ADD NEW Form80C Details ******************
        [HandleError]
        [HttpPost]
        public ActionResult CreateForm80CSettings(_TaxDeclFormSetting e)
        {
            if (Request.HttpMethod == "POST")
            {
                //_TaxDeclFormSetting _TaxDeclFormSetting= new _TaxDeclFormSetting();
                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_create", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Id", e.Id);
                        cmd.Parameters.AddWithValue("@LineNumber", e.LineNumber);
                        cmd.Parameters.AddWithValue("@SequenceNumber", e.SequenceNumber);
                        cmd.Parameters.AddWithValue("@Section", e.Section);
                        cmd.Parameters.AddWithValue("@SectionName", e.SectionName);
                        cmd.Parameters.AddWithValue("@Description", e.Description);
                        cmd.Parameters.AddWithValue("@ControlType", e.ControlType);
                        cmd.Parameters.AddWithValue("@MaximumValue", e.MaximumValue);
                        cmd.Parameters.AddWithValue("@AttachmentRequired", e.AttachmentRequired == null ? false : e.AttachmentRequired);
                        cmd.Parameters.AddWithValue("@AttachmentSize", e.AttachmentSize);
                        cmd.Parameters.AddWithValue("@AttachmentType", e.AttachmentType);
                        cmd.Parameters.AddWithValue("@EarningOrDeduction", e.EarningOrDeduction);
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            //return View();
            return RedirectToAction("Form80Csettingslist", "Tax");
        }

        // 1. *************show the list of Employee ******************
        // GET: Home
        [HandleError]
        public ActionResult Form80Csettingslist()
        {
            List<_TaxDeclFormSetting> _TaxDeclFormSettinglist = new List<_TaxDeclFormSetting>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_getall"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            _TaxDeclFormSettinglist.Add(new _TaxDeclFormSetting
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                LineNumber = Convert.ToString(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
                                Section = Convert.ToString(sdr["Section"]),
                                SectionName = Convert.ToString(sdr["SectionName"]),
                                Description = Convert.ToString(sdr["Description"]),
                                ControlType = Convert.ToInt32(sdr["ControlType"]),
                                MaximumValue = Convert.ToInt32(sdr["MaximumValue"]),
                                AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]),
                                AttachmentSize = Convert.ToString(sdr["AttachmentSize"]),
                                AttachmentType = Convert.ToString(sdr["AttachmentType"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (_TaxDeclFormSettinglist.Count == 0)
            {
                _TaxDeclFormSettinglist.Add(new _TaxDeclFormSetting());
            }
            return View(_TaxDeclFormSettinglist);
        }

        // GET: Home/Form80CSettingsDetails/5
        [HandleError]
        public ActionResult Form80CSettingsDetails(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_getbyid"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@TaxDeclFormSettingsid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            _TaxDeclFormSetting = new _TaxDeclFormSetting
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                LineNumber = Convert.ToString(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
                                Section = Convert.ToString(sdr["Section"]),
                                SectionName = Convert.ToString(sdr["SectionName"]),
                                Description = Convert.ToString(sdr["Description"]),
                                ControlType = Convert.ToInt32(sdr["ControlType"]),
                                MaximumValue = Convert.ToInt32(sdr["MaximumValue"]),
                                AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]),
                                AttachmentSize = Convert.ToString(sdr["AttachmentSize"]),
                                AttachmentType = Convert.ToString(sdr["AttachmentType"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (_TaxDeclFormSetting == null)
            {
                return HttpNotFound();
            }
            return View(_TaxDeclFormSetting);
        }


        // 3. *************Update Form80C Detail ******************

        // GET: Home/UpdateForm80C/5
        [HandleError]
        public ActionResult UpdateForm80CSettings(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Form80CSectionlist = GetLookup("Form80CSection");
            _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_getbyid"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@TaxDeclFormSettingsid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            _TaxDeclFormSetting = new _TaxDeclFormSetting
                            {

                                Id = Convert.ToInt32(sdr["Id"]),
                                LineNumber = Convert.ToString(sdr["LineNumber"]),
                                SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
                                Section = Convert.ToString(sdr["Section"]),
                                SectionName = Convert.ToString(sdr["SectionName"]),
                                Description = Convert.ToString(sdr["Description"]),
                                ControlType = Convert.ToInt32(sdr["ControlType"]),
                                MaximumValue = Convert.ToInt32(sdr["MaximumValue"]),
                                AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]),
                                AttachmentSize = Convert.ToString(sdr["AttachmentSize"]),
                                AttachmentType = Convert.ToString(sdr["AttachmentType"]),
                                EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (_TaxDeclFormSetting == null)
            {
                return HttpNotFound();
            }
            return View(_TaxDeclFormSetting);
        }

        // POST: Home/UpdateForm80C/5        
        [HandleError]
        [HttpPost]

        public ActionResult UpdateForm80CSettings([Bind(Include = "Id,LineNumber,SequenceNumber,Section,SectionName,Description,ControlType,MaximumValue,AttachmentRequired,AttachmentSize,AttachmentType,EarningOrDeduction")] _TaxDeclFormSetting _TaxDeclFormSetting)
        {
            if (ModelState.IsValid)
            {

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_update"))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", _TaxDeclFormSetting.Id);
                        cmd.Parameters.AddWithValue("@LineNumber", _TaxDeclFormSetting.LineNumber);
                        cmd.Parameters.AddWithValue("@SequenceNumber ", _TaxDeclFormSetting.SequenceNumber);
                        cmd.Parameters.AddWithValue("@Section", _TaxDeclFormSetting.Section);
                        cmd.Parameters.AddWithValue("@SectionName", _TaxDeclFormSetting.SectionName);
                        cmd.Parameters.AddWithValue("@Description", _TaxDeclFormSetting.Description);
                        cmd.Parameters.AddWithValue("@ControlType", _TaxDeclFormSetting.ControlType);
                        cmd.Parameters.AddWithValue("@MaximumValue", _TaxDeclFormSetting.MaximumValue);
                        cmd.Parameters.AddWithValue("@AttachmentRequired", _TaxDeclFormSetting.AttachmentRequired);
                        cmd.Parameters.AddWithValue("@AttachmentSize", _TaxDeclFormSetting.AttachmentSize);
                        cmd.Parameters.AddWithValue("@AttachmentType", _TaxDeclFormSetting.AttachmentType);
                        cmd.Parameters.AddWithValue("@EarningOrDeduction", _TaxDeclFormSetting.EarningOrDeduction);

                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                //return RedirectToAction("Form80Csettingslist");
            }
            //return View(_TaxDeclFormSetting);
            return RedirectToAction("Form80Csettingslist");
        }


        // <summary>  
        // Save Posted File in Physical path and return saved path to store in a database  
        // </summary>  
        // <param name = "file" ></ param >
        // < returns ></ returns >
        [HandleError]
        private string SaveToPhysicalLocation(HttpPostedFileBase file, int Empid, DateTime year)
        {
            //foreach (var file in files)
            //{
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string targetPath = Server.MapPath("~/App_Data/Uploads_" + Convert.ToString(Empid) + "_" + Convert.ToString(year.ToString("yyyy"))); //with complete path
                    if (!System.IO.Directory.Exists(targetPath))
                    {
                        System.IO.Directory.CreateDirectory(targetPath);
                    }

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Uploads_" + Convert.ToString(Empid) + "_" + Convert.ToString(year.ToString("yyyy"))), fileName);
                    file.SaveAs(path);
                    return path;
                }
                ///save image in sql server
                if (file.ContentType == "jpg")
                {

                }
                ///
            }
            //}
            return string.Empty;
        }



        // GET: ITCalculation
        [HandleError]
        [HttpGet]
        public ActionResult ITCalculation(int? id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Form80CHeader form80CHeader = new Form80CHeader();
            //List<_TaxDeclFormSetting> _TaxDeclFormSettings = new List<_TaxDeclFormSetting>();
            if (id == null)
                id = 1;

            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.ToList();

                if (employees != null)
                {
                    ViewBag.employees = employees;
                }
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_itcalc_getEmployee"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Address = Convert.ToString(sdr["Address"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.DepartmentName = Convert.ToString(sdr["DepartmentName"]);
                            form80CHeader.GradeName = Convert.ToString(sdr["GradeName"]);
                        }
                    }
                    con.Close();
                }

            }

            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

            //ITCALCULATION
            TaxCalculation taxCalculation = new TaxCalculation();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_TaxCalculations"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Parameters.AddWithValue("@year", DateTime.Now);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {


                            //taxCalculation.empid = Convert.ToInt32(sdr["Id"]);
                            //taxCalculation.empname = Convert.ToString(sdr["Name"]);
                            //taxCalculation.year = Convert.ToDateTime(sdr["Year"]);
                            taxCalculation.OldTaxone = Convert.ToDecimal(sdr["OldTaxone"]);
                            taxCalculation.OldTaxtwo = Convert.ToDecimal(sdr["OldTaxtwo"]);
                            taxCalculation.OldTaxthree = Convert.ToDecimal(sdr["OldTaxthree"]);
                            taxCalculation.OldTaxfour = Convert.ToDecimal(sdr["OldTaxfour"]);
                            taxCalculation.OldTotalTax = Convert.ToDecimal(sdr["OldTotalTax"]);
                            taxCalculation.OldEducationcess = Convert.ToDecimal(sdr["OldEducationcess"]);
                            taxCalculation.OldTaxOutGo = Convert.ToDecimal(sdr["OldTaxOutGo"]);
                            taxCalculation.Taxone = Convert.ToDecimal(sdr["Taxone"]);
                            taxCalculation.Taxtwo = Convert.ToDecimal(sdr["Taxtwo"]);
                            taxCalculation.Taxthree = Convert.ToDecimal(sdr["Taxthree"]);
                            taxCalculation.Taxfour = Convert.ToDecimal(sdr["Taxfour"]);
                            taxCalculation.Taxfive = Convert.ToDecimal(sdr["Taxfive"]);
                            taxCalculation.Taxsix = Convert.ToDecimal(sdr["Taxsix"]);
                            taxCalculation.Taxseven = Convert.ToDecimal(sdr["Taxseven"]);
                            taxCalculation.TotalTax = Convert.ToDecimal(sdr["TotalTax"]);
                            taxCalculation.Educationcess = Convert.ToDecimal(sdr["Educationcess"]);
                            taxCalculation.TaxOutGo = Convert.ToDecimal(sdr["TaxOutGo"]);
                            taxCalculation.earningpayslip = Convert.ToDecimal(sdr["earningpayslip"]);
                            taxCalculation.deductionpayslip = Convert.ToDecimal(sdr["deductionpayslip"]);
                            taxCalculation.earningform80 = Convert.ToDecimal(sdr["earningform80"]);                            
                            taxCalculation.AvancepaiIncometax = Convert.ToDecimal(sdr["AvancepaidIncometax"]);
                            taxCalculation.deductionform80 = Convert.ToDecimal(sdr["totaldeductionform80"]);
                            taxCalculation.Oldtaxableamount = Convert.ToDecimal(sdr["Oldtaxableamount"]);
                            taxCalculation.taxableAmount = Convert.ToDecimal(sdr["taxableAmount"]);
                            taxCalculation.HRADeductions = Convert.ToDecimal(sdr["LowestofHRArent"]);
                            taxCalculation.OtherDeductions = Convert.ToDecimal(sdr["deductionform80"]);
                            taxCalculation.standardDedusctions = Convert.ToDecimal(sdr["standarddeductions"]);

                        }
                    }
                    con.Close();
                }

            }
            //ViewBag.taxcalculations = taxCalculation;

            //selected Tax regime

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxRegime_Get"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    int regime = 3;
                    cmd.Parameters.AddWithValue("@Employeeid", id);
                    cmd.Parameters.AddWithValue("@MonthYear", DateTime.Now);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            regime = Convert.ToInt32(sdr["OldregimeOrNewregime"]);



                        }
                    }
                    con.Close();



                    if (regime == 0)
                    {
                        taxCalculation.isOldRegimeSelected = true;
                        taxCalculation.isNewRegimeSelected = false;
                    }
                    else if (regime == 1)
                    {
                        taxCalculation.isOldRegimeSelected = false;
                        taxCalculation.isNewRegimeSelected = true;

                    }
                    else
                    {
                        taxCalculation.isOldRegimeSelected = false;
                        taxCalculation.isNewRegimeSelected = false;
                    }
                }



            }
            ViewBag.taxcalculations = taxCalculation;

            //PAYSLIP         

            //using (SqlConnection con2 = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd2 = new SqlCommand("sp_paysilp_getbyempid"))
            //    {
            //        cmd2.Connection = con2;
            //        cmd2.Parameters.AddWithValue("@empid", id);
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        con2.Open();

            //        using (SqlDataReader sdr = cmd2.ExecuteReader())
            //        {
            //            while (sdr.Read())
            //            {
            //                if (sdr["MonthYear"].ToString() != "")
            //                {

            //                    PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
            //                    payslipGradeEntry.EmployeeId = Convert.ToString(sdr["EmployeeId"]);
            //                    payslipGradeEntry.EmpId = Convert.ToInt32(sdr["Id"]);
            //                    payslipGradeEntry.MonthYear = Convert.ToDateTime(sdr["MonthYear"]);
            //                    payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
            //                    payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
            //                    payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
            //                    payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
            //                    payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
            //                }
            //            }
            //        }
            //        con2.Close();
            //    }
            //    if (payslipGradeHeader.PayslipGradeEntryList.Count == 0)
            //    {
            //        payslipGradeHeader.PayslipGradeEntryList.Add(new PayslipGradeEntry());
            //    }
            //}
            //ViewBag.payslips = payslipGradeHeader.PayslipGradeEntryList;
            string payslip = "sp_paysilp_getbyempid1";
            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {

                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@empid", id);

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
                            form80CHeader.ReportRowList.Add(oReportRow);
                        }
                    }
                    con2.Close();


                }
            }
            return View(form80CHeader);
        }

        // GET: ITCalculation
        [HandleError]
        [HttpPost]
        public ActionResult ITCalculation(FormCollection form)
        {

            decimal txt_OldTaxOutGo = Convert.ToDecimal(form["txt_OldTaxOutGo"].ToString());
            decimal txt_TaxOutGo = Convert.ToDecimal(Request.Form["txt_TaxOutGo"].ToString());
            int txt_Employeeid = Convert.ToInt32(form["txt_Employeeid"].ToString());
            string txt_AvancepaiIncometax = form["txt_AvancepaiIncometax"].ToString();
            string rdo_oldORnewRegime = form["rdo_oldORnewRegime"].ToString();
            int id = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TaxRegime_create", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Employeeid", txt_Employeeid);
                    cmd.Parameters.AddWithValue("@MonthYear", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TaxCollected", txt_AvancepaiIncometax);
                    cmd.Parameters.AddWithValue("@OldTaxProjection", txt_OldTaxOutGo);
                    cmd.Parameters.AddWithValue("@NewTaxProjection", txt_TaxOutGo);
                    cmd.Parameters.AddWithValue("@OldregimeOrNewregime", rdo_oldORnewRegime);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", Convert.ToInt32(Session["Id"]));

                    con.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();
                    con.Close();
                }
            }
            if (Convert.ToString(Session["AccessType"]).ToUpper() == "ADMIN")
            {
                return RedirectToAction("Details", "Employee");
            }
            else
            {
                return RedirectToAction("EmployeeDetails", "Employee", new { id = txt_Employeeid });
            }
        }

        // convert image to byte array
        [HandleError]
        public byte[] imageToByteArray(HttpPostedFileBase imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.InputStream.CopyTo(ms);
            return ms.ToArray();
        }

        //Byte array to photo
        [HandleError]
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        // 1. *************Get Selection in dropdown******************
        // GET: Home
        [HandleError]
        public List<Empalldropdown> GetLookup(string type)
        {
            List<Empalldropdown> Empalldropdownlist = new List<Empalldropdown>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Empalldropdown_getbytype"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.CommandType = CommandType.StoredProcedure;
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


        // 3. *************Delete Form80C******************

        // GET: Home/DeleteForm80C/5
        //public ActionResult DeleteForm80CSettings(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    _TaxDeclFormSetting _TaxDeclFormSetting = new _TaxDeclFormSetting();
        //    string query = "SELECT * FROM [TaxDeclFormSettings] where Id=" + id;
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
        //                    _TaxDeclFormSetting = new _TaxDeclFormSetting
        //                    {
        //                        Id = Convert.ToInt32(sdr["Id"]),
        //                        LineNumber = Convert.ToString(sdr["LineNumber"]),
        //                        SequenceNumber = Convert.ToDecimal(sdr["SequenceNumber"]),
        //                        Section = Convert.ToString(sdr["Section"]),
        //                        SectionName = Convert.ToString(sdr["SectionName"]),
        //                        Description = Convert.ToString(sdr["Description"]),
        //                        ControlType = Convert.ToInt32(sdr["ControlType"]),
        //                        MaximumValue = Convert.ToInt32(sdr["MaximumValue"]),
        //                        AttachmentRequired = Convert.ToBoolean(sdr["AttachmentRequired"]),
        //                        AttachmentSize = Convert.ToString(sdr["AttachmentSize"]),
        //                        AttachmentType = Convert.ToString(sdr["AttachmentType"]),
        //                        EarningOrDeduction = Convert.ToInt32(sdr["EarningOrDeduction"])

        //                    };
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    return View(_TaxDeclFormSetting);
        //}

        // POST: Home/DeleteForm80CSettings/5
        [HandleError]
        [HttpGet, ActionName("DeleteForm80CSettings")]
        //[ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult DeleteForm80CSettings(int id)
        {
            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_TaxDeclFormSettings_delete", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ViewBag.AlertMsg = "Record Deleted Successfully";
                }
                return RedirectToAction("Form80Csettingslist");
            }
            catch
            {
                return View();
            }
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


        // GET: Tax on Submission
        [HandleError]
        [HttpGet]
        public ActionResult ITCalcDeclaration(int? id)
        {

            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Form80CHeader form80CHeader = new Form80CHeader();
            //List<_TaxDeclFormSetting> _TaxDeclFormSettings = new List<_TaxDeclFormSetting>();
            if (id == null)
                id = 1;

            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.Where(s => s.Availability == "Active").ToList();
                List<Setting> Settings = db.Settings.ToList();


                if (employees != null)
                {
                    ViewBag.employees = employees;
                }

                if (Settings != null)
                {
                    ViewBag.Settings = Settings;
                }
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_itcalc_getEmployee"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Address = Convert.ToString(sdr["Address"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.DepartmentName = Convert.ToString(sdr["DepartmentName"]);
                            form80CHeader.GradeName = Convert.ToString(sdr["GradeName"]);
                        }
                    }
                    con.Close();
                }

            }

            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

            //ITCALCULATION
            TaxCalculation taxCalculation = new TaxCalculation();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_TaxCalculations_Declaredamt"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Parameters.AddWithValue("@year", DateTime.Now);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {


                            //taxCalculation.empid = Convert.ToInt32(sdr["Id"]);
                            //taxCalculation.empname = Convert.ToString(sdr["Name"]);
                            //taxCalculation.year = Convert.ToDateTime(sdr["Year"]);
                            taxCalculation.OldTaxone = Convert.ToDecimal(sdr["OldTaxone"]);
                            taxCalculation.OldTaxtwo = Convert.ToDecimal(sdr["OldTaxtwo"]);
                            taxCalculation.OldTaxthree = Convert.ToDecimal(sdr["OldTaxthree"]);
                            taxCalculation.OldTaxfour = Convert.ToDecimal(sdr["OldTaxfour"]);
                            taxCalculation.OldTotalTax = Convert.ToDecimal(sdr["OldTotalTax"]);
                            taxCalculation.OldEducationcess = Convert.ToDecimal(sdr["OldEducationcess"]);
                            taxCalculation.OldTaxOutGo = Convert.ToDecimal(sdr["OldTaxOutGo"]);
                            taxCalculation.Taxone = Convert.ToDecimal(sdr["Taxone"]);
                            taxCalculation.Taxtwo = Convert.ToDecimal(sdr["Taxtwo"]);
                            taxCalculation.Taxthree = Convert.ToDecimal(sdr["Taxthree"]);
                            taxCalculation.Taxfour = Convert.ToDecimal(sdr["Taxfour"]);
                            taxCalculation.Taxfive = Convert.ToDecimal(sdr["Taxfive"]);
                            taxCalculation.Taxsix = Convert.ToDecimal(sdr["Taxsix"]);
                            taxCalculation.Taxseven = Convert.ToDecimal(sdr["Taxseven"]);
                            taxCalculation.TotalTax = Convert.ToDecimal(sdr["TotalTax"]);
                            taxCalculation.Educationcess = Convert.ToDecimal(sdr["Educationcess"]);
                            taxCalculation.TaxOutGo = Convert.ToDecimal(sdr["TaxOutGo"]);
                            taxCalculation.earningpayslip = Convert.ToDecimal(sdr["earningpayslip"]);
                            taxCalculation.deductionpayslip = Convert.ToDecimal(sdr["deductionpayslip"]);
                            taxCalculation.earningform80 = Convert.ToDecimal(sdr["earningform80"]);
                            taxCalculation.deductionform80 = Convert.ToDecimal(sdr["totaldeductionform80"]);
                            taxCalculation.Oldtaxableamount = Convert.ToDecimal(sdr["Oldtaxableamount"]);
                            taxCalculation.taxableAmount = Convert.ToDecimal(sdr["taxableAmount"]);
                            taxCalculation.HRADeductions = Convert.ToDecimal(sdr["LowestofHRArent"]);
                            taxCalculation.OtherDeductions = Convert.ToDecimal(sdr["deductionform80"]);
                            taxCalculation.standardDedusctions = Convert.ToDecimal(sdr["standarddeductions"]);

                        }
                    }
                    con.Close();
                }

            }
            ViewBag.taxcalculations = taxCalculation;

            //PAYSLIP

            //using (SqlConnection con2 = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd2 = new SqlCommand("sp_paysilp_getbyempid"))
            //    {
            //        cmd2.Connection = con2;
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        cmd2.Parameters.AddWithValue("@empid", id);
            //        con2.Open();

            //        using (SqlDataReader sdr = cmd2.ExecuteReader())
            //        {
            //            while (sdr.Read())
            //            {
            //                if (sdr["MonthYear"].ToString() != "")
            //                {

            //                    PayslipGradeEntry payslipGradeEntry = new PayslipGradeEntry();
            //                    payslipGradeEntry.EmployeeId = Convert.ToString(sdr["EmployeeId"]);
            //                    payslipGradeEntry.EmpId = Convert.ToInt32(sdr["Id"]);
            //                    payslipGradeEntry.MonthYear = Convert.ToDateTime(sdr["MonthYear"]);
            //                    payslipGradeEntry.Description = Convert.ToString(sdr["Description"]);
            //                    payslipGradeEntry.MonthlyAmount = Convert.ToDecimal(sdr["MonthlyAmount"]);
            //                    payslipGradeEntry.AnnualAmount = Convert.ToDecimal(sdr["AnnualAmount"]);
            //                    payslipGradeEntry.PayslipGradeid = Convert.ToInt32(sdr["gradeid"]);
            //                    payslipGradeHeader.PayslipGradeEntryList.Add(payslipGradeEntry);
            //                }
            //            }
            //        }
            //        con2.Close();
            //    }
            //    if (payslipGradeHeader.PayslipGradeEntryList.Count == 0)
            //    {
            //        payslipGradeHeader.PayslipGradeEntryList.Add(new PayslipGradeEntry());
            //    }
            //}
            //ViewBag.payslips = payslipGradeHeader.PayslipGradeEntryList;

            List<PayslipGradeHeader> payslipGradeHeaderList = new List<PayslipGradeHeader>();

           // Form80CHeader oPaySlipReport = new Form80CHeader();
            string payslip = "sp_paysilp_getbyempid1";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {

                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@empid", id);

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
                            form80CHeader.ReportRowList.Add(oReportRow);
                        }
                    }
                    con2.Close();


                }
            }           
            return View(form80CHeader);
        }


        //GET: Tax Declaration Calculation 2023 New Regime
        [HandleError]
        [HttpGet]
        public ActionResult ITCalcDeclaration2023(int? id)
        {

            if (Convert.ToInt32(Session["Id"]) != id && Convert.ToString(Session["AccessType"]).ToUpper() != "ADMIN")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Form80CHeader form80CHeader = new Form80CHeader();            
            if (id == null)
                id = 1;

            using (AppEntities1 db = new AppEntities1())
            {
                List<Emp> employees = db.Emps.Where(s => s.Availability == "Active").ToList();
                List<Setting> Settings = db.Settings.ToList();


                if (employees != null)
                {
                    ViewBag.employees = employees;
                }

                if (Settings != null)
                {
                    ViewBag.Settings = Settings;
                }
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_itcalc_getEmployee"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            form80CHeader.Empid = Convert.ToInt32(sdr["Id"]);
                            form80CHeader.Employeeid = Convert.ToString(sdr["Employeeid"]);
                            form80CHeader.EmployeeName = Convert.ToString(sdr["Name"]);
                            form80CHeader.DateofJoin = Convert.ToDateTime(sdr["DateofJoin"]);
                            form80CHeader.Gender = Convert.ToString(sdr["Gender"]);
                            form80CHeader.DOB = Convert.ToDateTime(sdr["DOB"]);
                            form80CHeader.PAN = Convert.ToString(sdr["PAN"]);
                            form80CHeader.Address = Convert.ToString(sdr["Address"]);
                            form80CHeader.Bankname = Convert.ToString(sdr["Bankname"]);
                            form80CHeader.Bankacno = Convert.ToString(sdr["Bankacno"]);
                            form80CHeader.PFAccountNo = Convert.ToString(sdr["PFAccountNo"]);
                            form80CHeader.Designation = Convert.ToString(sdr["Designation"]);
                            form80CHeader.DesignationName = Convert.ToString(sdr["DesignationName"]);
                            form80CHeader.DepartmentName = Convert.ToString(sdr["DepartmentName"]);
                            form80CHeader.GradeName = Convert.ToString(sdr["GradeName"]);
                        }
                    }
                    con.Close();
                }

            }

            PayslipGradeHeader payslipGradeHeader = new PayslipGradeHeader();
            payslipGradeHeader.PayslipGradeEntryList = new List<PayslipGradeEntry>();

            //ITCALCULATION
            TaxCalculation taxCalculation = new TaxCalculation();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_TaxCalculations_Declaredamt_2023"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empid", id);
                    cmd.Parameters.AddWithValue("@year", DateTime.Now);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            taxCalculation.HRADeductions = Convert.ToDecimal(sdr["LowestofHRArent"]);
                            taxCalculation.OtherDeductions = Convert.ToDecimal(sdr["deductionform80"]);
                            taxCalculation.standardDedusctions = Convert.ToDecimal(sdr["standarddeductions"]);
                            taxCalculation.earningpayslip = Convert.ToDecimal(sdr["earningpayslip"]);
                            taxCalculation.deductionpayslip = Convert.ToDecimal(sdr["deductionpayslip"]);
                            taxCalculation.earningform80 = Convert.ToDecimal(sdr["earningform80"]);
                            taxCalculation.deductionform80 = Convert.ToDecimal(sdr["totaldeductionform80"]);
                            taxCalculation.Oldtaxableamount = Convert.ToDecimal(sdr["Oldtaxableamount"]);
                            taxCalculation.taxableAmount = Convert.ToDecimal(sdr["taxableAmount"]);
                            taxCalculation.TotalTax = Convert.ToDecimal(sdr["TotalTax"]);
                            taxCalculation.Educationcess = Convert.ToDecimal(sdr["Educationcess"]);
                            taxCalculation.TaxOutGo = Convert.ToDecimal(sdr["TaxOutGo"]);                            
                            taxCalculation.Taxone = Convert.ToDecimal(sdr["Taxone"]);
                            taxCalculation.Taxtwo = Convert.ToDecimal(sdr["Taxtwo"]);
                            taxCalculation.Taxthree = Convert.ToDecimal(sdr["Taxthree"]);
                            taxCalculation.Taxfour = Convert.ToDecimal(sdr["Taxfour"]);
                            taxCalculation.Taxfive = Convert.ToDecimal(sdr["Taxfive"]);
                           taxCalculation.Taxsix = Convert.ToDecimal(sdr["Taxsix"]);
                            taxCalculation.OldTaxone = Convert.ToDecimal(sdr["OldTaxone"]);
                            taxCalculation.OldTaxtwo = Convert.ToDecimal(sdr["OldTaxtwo"]);
                            taxCalculation.OldTaxthree = Convert.ToDecimal(sdr["OldTaxthree"]);
                            taxCalculation.OldTaxfour = Convert.ToDecimal(sdr["OldTaxfour"]);
                            taxCalculation.OldTotalTax = Convert.ToDecimal(sdr["OldTotalTax"]);
                            taxCalculation.OldEducationcess = Convert.ToDecimal(sdr["OldEducationcess"]);
                            taxCalculation.OldTaxOutGo = Convert.ToDecimal(sdr["OldTaxOutGo"]);
                        }
                    }
                    con.Close();
                }

            }
            ViewBag.taxcalculations = taxCalculation;            

            List<PayslipGradeHeader> payslipGradeHeaderList = new List<PayslipGradeHeader>();

            string payslip = "sp_paysilp_getbyempid1";

            using (SqlConnection con2 = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand(payslip))
                {

                    ReportRow oReportRow = new ReportRow();
                    cmd2.Connection = con2;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@empid", id);

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
                            form80CHeader.ReportRowList.Add(oReportRow);
                        }
                    }
                    con2.Close();


                }
            }
            return View(form80CHeader);
        }
    }
}

