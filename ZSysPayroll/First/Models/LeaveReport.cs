using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class LeaveReport
    {
        public int id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public int FinancialYear { get; set; }
        public decimal OpeningLeaveCasual { get; set; }
        public decimal TakenTillCasual { get; set; }
        public decimal TakenTillSick { get; set; }
        public decimal TakenTillToal { get; set; }
        public decimal EligibleCasual { get; set; }
        public decimal EligibleSick { get; set; }
        public decimal BalanceCasual { get; set; }
        public decimal BalanceSick { get; set; }
    }
}