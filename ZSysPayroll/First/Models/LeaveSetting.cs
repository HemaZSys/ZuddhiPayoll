using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class LeaveSettingHeader
    {        
        public int id { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveAbbreviation { get; set; }
        public string LeaveDescription { get; set; }
        public decimal NoOfDays { get; set; }     
    }
}