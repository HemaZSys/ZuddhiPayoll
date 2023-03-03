using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class LeaveDetailsHeader
    {
        public List<EmployeeLeave> EmployeeLeaveList { get; set; }

        public LeaveDetailsHeader()
        {
            EmployeeLeaveList = new List<EmployeeLeave>();
        }
    }
}