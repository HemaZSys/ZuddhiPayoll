using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class LeaveReportHeader
    { 
        public List<LeaveReport> LeaveReportList { get; set; }

        public LeaveReportHeader()
        {
            LeaveReportList = new List<LeaveReport>();
        }
    }
}