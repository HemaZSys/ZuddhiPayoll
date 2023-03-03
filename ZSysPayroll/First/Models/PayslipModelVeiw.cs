using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class PayslipModelVeiw
    {
        //employee
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

       
        [Display(Name = "Designation")]
        public string Designation { get; set; }

       
        [Display(Name = "Date of Join")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public string DateofJoin { get; set; }

        [Display(Name = "Gender")]
        
        public string Gender { get; set; }

        [Display(Name = "Education")]
        
        public string Education { get; set; }

        [Display(Name = "Address")]       
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Contact { get; set; }

        [Display(Name = "PAN")]
        [Required(ErrorMessage = "Please enter PAN Number")]
        public string PAN { get; set; }

        [Display(Name = "Aadhar")]
        [Required(ErrorMessage = "Please enter Aadhar Number")]
        public string Aadhar { get; set; }

        [Display(Name = "Passport")]
        [Required(ErrorMessage = "Please enter Passport Number")]
        public string Passport { get; set; }
        //payslip
        [Display(Name = "PId")]
        public int PId { get; set; }

        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Basic is required.")]
        public string Basic { get; set; }

        [Required(ErrorMessage = "HRA is required.")]       
        public string HRA { get; set; }

        [Required(ErrorMessage = "DA is required.")]        
        public string DA { get; set; }

        [Required(ErrorMessage = "Conveyance is required.")]
        public string Coveyance { get; set; }

        [Required(ErrorMessage = "Bonus is required.")]
        public string Bonus { get; set; }    

        [Required(ErrorMessage = "PF is required.")]
        public string PF { get; set; }

        [Required(ErrorMessage = "IncomeTax is required.")]
        public string Incometax { get; set; }

        [Required(ErrorMessage = "ProfessionTax is required.")]
        public string Professiontax { get; set; }

        [Required(ErrorMessage = "Salary advance is required.")]
        public string SalaryAdv { get; set; } 
        
        public string Others { get; set; }       
        public string Total_Sal { get; set; }

        [Required(ErrorMessage = "Select the Month.")]        
        public DateTime Month { get; set; }
        [Required]
        [Display(Name = "Month")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{Month}")]
        public string Monthstring { get; set; }

        [Required(ErrorMessage = "Gross_Total")]
        public string Gross_Total { get; set; }

        [Required(ErrorMessage = "Dedution_Total")]
        public string Deduction_Total { get; set; }       

        public List<PayslipModelVeiw> Payslipinfo { get; set; }
    }
}