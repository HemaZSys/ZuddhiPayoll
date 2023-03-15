using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class EmployeeLeave
    {

        public EmployeeLeave()
        {
            LeaveType = 1;
            startDate = System.DateTime.Today.AddDays(1);
            if (startDate.DayOfWeek.ToString() == "5")
                startDate = startDate.AddDays(2);
            else if (startDate.DayOfWeek.ToString() == "6")
                startDate = startDate.AddDays(1);

            if (EndDate.DayOfWeek.ToString() == "5")
                EndDate = EndDate.AddDays(-1);
            else if (EndDate.DayOfWeek.ToString() == "6")
                EndDate = EndDate.AddDays(-2);
        }

        [Display(Name = "ID")]
        public int Id { get; set; }
        public int LeaveId { get; set; }
        public int sessionId { get; set; }

        [Display(Name = "Leave Type")]
        //[Required(ErrorMessage = "Please select the Gender")]
        public int LeaveType { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }
        public string EmpName { get; set; }

        public string UserEmailId { get; set; }
        public string ReportingMgrEmailID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime startDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public decimal NoOfDays { get; set; }

        public string LeaveStatus { get; set; }

        public string LeaveReason { get; set; }
        public string Reportingmanager { get; set; }

        public int LeaveBalance { get; set; }

        public bool IsApproved { get; set; }
        public string ApproveAction { get; set; }

        public int ReportingmanagerId { get; set; }
        public int CasualLeaveBalance { get; set; }
        public int SickLeaveBalance { get; set; }
    }
}