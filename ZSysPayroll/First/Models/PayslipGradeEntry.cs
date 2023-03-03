namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class PayslipGradeEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int PayslipGradeid { get; set; }

        public DateTime MonthYear { get; set; }

        public string EmployeeId { get; set; }

        public int EmpId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeGrade { get; set; }

        public decimal EmployeeGrosspay { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal MonthlyAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal AnnualAmount { get; set; }

        public decimal LOP { get; set; }

        public int LocationID { get; set; }

        public int DepartmentID { get; set; }

        public int DesignationID { get; set; }

        public String Location { get; set; }

        public String Department { get; set; }

        public String Designation { get; set; }


        public string PFAccountNo { get; set; }


        public DateTime DOJ { get; set; }

        public DateTime DOC { get; set; }

        public string GradeName { get; set; }

        public int LineNumber { get; set; }

        public int SequenceNumber { get; set; }

        public string SectionDescription { get; set; }

        public string Description { get; set; }
        
        public int EarningOrDeduction { get; set; }

        public decimal Percentage { get; set; }
        public int taxcollected { get; set; }
       
    }
}
