using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class ViewModel
    {
        public Emp Employee { get; set; }
        public Payslip Payslip { get; set; }
        public Payslip1 Payslip1 { get; set; }

        public PayslipGradeEntry PayslipGradeEntry { get; set; }
    }
}