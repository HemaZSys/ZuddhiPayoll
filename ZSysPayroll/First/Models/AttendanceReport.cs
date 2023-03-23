using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class AttendanceReport
    {
        public int id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public string shiftDate { get; set; }
       
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:tt}", ApplyFormatInEditMode = true)]
        public string LogInTime { get; set; }
        [DataType(DataType.Time)]
        public string LogOutTime { get; set; }
        [DataType(DataType.Time)]
        public string TotalTime { get; set; }
        public string WorkStatus { get; set; }
    }
}