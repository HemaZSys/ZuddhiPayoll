using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class MonthlyAttendanceReport
    {
        public List<ReportRow> ReportRowList { get; set; }
        public DateTime MonthYear { get; set; }
        public MonthlyAttendanceReport()
        {
            ReportRowList = new List<ReportRow>();
        }
    }
}