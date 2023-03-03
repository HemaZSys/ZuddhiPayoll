using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class PayslipGradeHeader
    {
        public DateTime DOB;

        public string Id { get; set; }
        public int EmpId { get; set; }

        public int PayslipHeaderid { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime MonthYear { get; set; }

        public decimal LOP { get; set; }

        public decimal LeavesTaken { get; set; }
        public decimal NetSalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalDeductions { get; set; }
        public string EmployeeGrade { get; set; }

        public int LocationID { get; set; }

        public int DepartmentID { get; set; }

        public int DesignationID { get; set; }

        public String Location { get; set; }

        public String Department { get; set; }

        public String Designation { get; set; }



        public string PFAccountNo { get; set; }


        public DateTime DOJ { get; set; }

        public DateTime DOC { get; set; }


        public string EmployeeGrosspay { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal taxcollected { get; set; }
        public string CompanyName { get; set; }
        public List<PayslipGradeEntry> PayslipGradeEntryList { get; set; }

        public List<PayslipGrade> PayslipGradeList { get; set; }

        public List<Employee> EmployeeList { get; set; }

        public List<PayslipGradeHeader> listPayslip { get; set; }
        public int PaymentModeid { get; set; }
        public string PaymentMode { get; set; }

        public decimal TaxCollectedfromRegime { get; set; }
        public decimal OldTaxProjection { get; set; }
        public decimal NewTaxProjection { get; set; }
        public decimal OldregimeOrNewregime { get; set; }
        public decimal Ranking { get; set; }
        public DateTime DateofJoin { get; internal set; }
        public bool isSalaryStructure { get; set; }

        //For PayslipReport
        public string Description { get; set; }
        public string SectionDescription { get; set; }
        public string MonthlyAmount { get; set; }
        public string EmployeeGradeID { get; set; }
    }
}