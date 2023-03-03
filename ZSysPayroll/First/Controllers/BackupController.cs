using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace First.Controllers
{
    public class BackupController : Controller
    {
        // GET: Backup  

        public ActionResult BackupView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BackupView()
        {
            public ActionResult Index() => View();
            public async Task<JsonResult> GenerateBackupFile()
            {
                try
                {
                    await Task.Run(() =>
                    {
                        string dbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        string backupFolderName = ConfigurationManager.AppSettings["BackUpFolder"].ToString();
                        if (!Directory.Exists(backupFolderName))
                            Directory.CreateDirectory(backupFolderName);
                        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnectionString);
                        var backupFileName = $"{backupFolderName}{sqlConnectionStringBuilder.InitialCatalog}-{DateTime.Now.ToString("yyyy-MM-dd")}.bak";
                        if (System.IO.File.Exists(backupFileName))
                            System.IO.File.Delete(backupFileName);
                        using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                        {
                            string backupQuery = $"BACKUP DATABASE {sqlConnectionStringBuilder.InitialCatalog} TO DISK='{backupFileName}'";
                            using (SqlCommand command = new SqlCommand(backupQuery, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                    });
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}