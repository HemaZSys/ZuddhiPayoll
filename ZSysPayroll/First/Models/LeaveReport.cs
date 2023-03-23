using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class LeaveReport
    {
        public int id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public int FinancialYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal OpeningLeaveCasual { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal TakenTillCasual { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal TakenTillSick { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal TakenTillToal { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal EligibleCasual { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal EligibleSick { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal BalanceCasual { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal BalanceSick { get; set; }
    }
}