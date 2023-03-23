using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class AttendanceReportHeader
    {
        public List<AttendanceReport> AttendanceReportList { get; set; }

        public AttendanceReportHeader()
        {
            AttendanceReportList = new List<AttendanceReport>();
        }
    }
}