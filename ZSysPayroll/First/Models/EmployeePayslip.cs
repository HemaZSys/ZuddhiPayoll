using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace First.Models
{
    public class EmployeePayslip
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name="Status")]
        public string Availability { get; set; }

        [Required(ErrorMessage = "Please enter Employee Name")]
        [Display(Name = "Emp Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Designation")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "EmpID")]       
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Join")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateofJoin { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "Gender")]
        //[Required(ErrorMessage = "Please select the Gender")]
        public string Gender { get; set; }

        
        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Please Select the Education Qualification")]
        public string Qualification { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid Phone Number.")]
        public string Contact { get; set; }
        
        [Display(Name = "PAN")]
        [RegularExpression(@"^([A-Z]{5}[0-9]{4}[A-Z]{1})$", ErrorMessage = "Please enter valid 10 digit PAN Number")]
        [Required(ErrorMessage = "Please enter 10 digit PAN Number")]
        public string PAN { get; set; }

        [Display(Name = "Aadhar")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Please enter 12 digit Aadhar Number")]
        [Required(ErrorMessage = "Please enter 12 digit Aadhar Number")]
        public string Aadhar { get; set; }

        [Display(Name = "Passport")]
        [Required(ErrorMessage = "Please enter Passport Number")]
        public string Passport { get; set; }

             

        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Please enter Grade")]
        public string Grade { get; set; }        

        [Display(Name = "Age")]
        public int Age { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Modified { get; set; }

        [Display(Name = "Email")]
        [Required]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string Email { get; set; }

        [Display(Name = "LName")]
        public string LName { get; set; }

        [Display(Name = "Alternative Number")]
        public string Alternativeno { get; set; }

        [Display(Name = "Official Email")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string offcEmail { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Job Type")]
        public string Empstatus { get; set; }

        [Display(Name = "Reporting Manager")]
        public string Reportingmanager { get; set; }

        [Display(Name = "Previous company name")]
        public string Precompanyname { get; set; }

        [Display(Name = "Previous Designation")]
        public string Predesignation { get; set; }

         [Display(Name = "Previous Experience")]
         public string Preexperience { get; set; }

        [Display(Name = "Bank Name")]
        public string Bankname { get; set; }

        [Display(Name = "Account Number")]
        public string Bankacno { get; set; }

        [Display(Name = "Bank Branch")]
        public string Bankbranch { get; set; }

        [Display(Name = "Bank IFSC Code")]
        public string Bankifsc { get; set; }

        [Display(Name = "Marital Status")]
        public string Maritalstatus { get; set; }

        [Display(Name = "Bloodgroup")]
        public string BloodGroup { get; set; }

        [Display(Name = "Gross Pay")]
        public decimal Grosspay { get; set; }
        public List<Employee> Employeesinfo { get; set; }

        [Display(Name = "PF Account No. ")]
        public string PFAccountNo { get; set; }

        [Display(Name = "Month Year")]
        public string MonYear { get; set; }

        [Display(Name = "LOP")]
        public decimal LOP { get; set; }

        [Display(Name = "Leaves Taken")]
        public decimal LeavesTaken { get; set; }

        [Display(Name = "Leaves Accumulated")]
        public decimal LeavesAccumulated { get; set; }

        [Display(Name = "Leaves Available")]
        public decimal LeavesAvailable { get; set; }

        


        public EmployeePayslip()
        {
            Gender = "Male";
            Maritalstatus = "Married";
            Availability = "Active";
        }
    }
}