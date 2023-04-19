using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class AttendanceReportHeader
    {
        public string EmpName { get; set; }
        public DateTime MonthYear { get; set; }
        public List<AttendanceReport> AttendanceReportList { get; set; }

        public AttendanceReportHeader()
        {
            AttendanceReportList = new List<AttendanceReport>();
        }
    }
}