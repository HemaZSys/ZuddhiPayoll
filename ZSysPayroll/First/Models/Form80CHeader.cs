using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace First.Models
{
    public class Form80CHeader
    {
        #region Properties
        public string MyProperty { get; set; }
        public int headerid { get; set; }
        public string Employeeid { get; set; }
        public string PAN { get; set; }
        public string Bankname { get; set; }
        public string PFAccountNo { get; set; }
        public string Bankacno { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateofJoin { get; set; }
        public string Designation { get; set; }
        public string DesignationName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }       

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DOB { get; set; }

        public int Empid { get; set; }
        public string EmployeeName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime MonthYear { get; set; }

        public decimal Ranking { get; set; }
        public string AprilMetro { get; set; }
        public string MayMetro { get; set; }
        public string JuneMetro { get; set; }
        public string JulyMetro { get; set; }
        public string AugustMetro { get; set; }
        public string SeptemberMetro { get; set; }
        public string OctoberMetro { get; set; }
        public string NovemberMetro { get; set; }
        public string DecemberMetro { get; set; }
        public string JanuaryMetro { get; set; }
        public string FebruaryMetro { get; set; }
        public string MarchMetro { get; set; }
        public string AprilProofSubmission { get; set; }
        public string MayProofSubmission { get; set; }
        public string JuneProofSubmission { get; set; }
        public string JulyProofSubmission { get; set; }
        public string AugustProofSubmission { get; set; }
        public string SeptemberProofSubmission { get; set; }
        public string OctoberProofSubmission { get; set; }
        public string NovemberProofSubmission { get; set; }
        public string DecemberProofSubmission { get; set; }
        public string JanuaryProofSubmission { get; set; }
        public string FebruaryProofSubmission { get; set; }
        public string MarchProofSubmission { get; set; }
        public string AprilNonMetro { get; set; }
        public string MayNonMetro { get; set; }
        public string JuneNonMetro { get; set; }
        public string JulyNonMetro { get; set; }
        public string AugustNonMetro { get; set; }
        public string SeptemberNonMetro { get; set; }
        public string OctoberNonMetro { get; set; }
        public string NovemberNonMetro { get; set; }
        public string DecemberNonMetro { get; set; }
        public string JanuaryNonMetro { get; set; }
        public string FebruaryNonMetro { get; set; }
        public string MarchNonMetro { get; set; }
        public string AprilNonMetroProofSubmission { get; set; }
        public string MayNonMetroProofSubmission { get; set; }
        public string JuneNonMetroProofSubmission { get; set; }
        public string JulyNonMetroProofSubmission { get; set; }
        public string AugustNonMetroProofSubmission { get; set; }
        public string SeptemberNonMetroProofSubmission { get; set; }
        public string OctoberNonMetroProofSubmission { get; set; }
        public string NovemberNonMetroProofSubmission { get; set; }
        public string DecemberNonMetroProofSubmission { get; set; }
        public string JanuaryNonMetroProofSubmission { get; set; }
        public string FebruaryNonMetroProofSubmission { get; set; }
        public string MarchNonMetroProofSubmission { get; set; }
        public string AprilRemarks { get; set; }
        public string MayRemarks { get; set; }
        public string JuneRemarks { get; set; }
        public string JulyRemarks { get; set; }
        public string AugustRemarks { get; set; }
        public string SeptemberRemarks { get; set; }
        public string OctoberRemarks { get; set; }
        public string NovemberRemarks { get; set; }
        public string DecemberRemarks { get; set; }
        public string JanuaryRemarks { get; set; }
        public string FebruaryRemarks { get; set; }
        public string MarchRemarks { get; set; }
        public string AprilLandlordPAN { get; set; }
        public string MayLandlordPAN { get; set; }
        public string JuneLandlordPAN { get; set; }
        public string JulyLandlordPAN { get; set; }
        public string AugustLandlordPAN { get; set; }
        public string SeptemberLandlordPAN { get; set; }
        public string OctoberLandlordPAN { get; set; }
        public string NovemberLandlordPAN { get; set; }
        public string DecemberLandlordPAN { get; set; }
        public string JanuaryLandlordPAN { get; set; }
        public string FebruaryLandlordPAN { get; set; }
        public string MarchLandlordPAN { get; set; }
        public string AprilLandlordName { get; set; }
        public string MayLandlordName { get; set; }
        public string JuneLandlordName { get; set; }
        public string JulyLandlordName { get; set; }
        public string AugustLandlordName { get; set; }
        public string SeptemberLandlordName { get; set; }
        public string OctoberLandlordName { get; set; }
        public string NovemberLandlordName { get; set; }
        public string DecemberLandlordName { get; set; }
        public string JanuaryLandlordName { get; set; }
        public string FebruaryLandlordName { get; set; }
        public string MarchLandlordName { get; set; }
        public string AprilLandlordAddress { get; set; }
        public string MayLandlordAddress { get; set; }
        public string JuneLandlordAddress { get; set; }
        public string JulyLandlordAddress { get; set; }
        public string AugustLandlordAddress { get; set; }
        public string SeptemberLandlordAddress { get; set; }
        public string OctoberLandlordAddress { get; set; }
        public string NovemberLandlordAddress { get; set; }
        public string DecemberLandlordAddress { get; set; }
        public string JanuaryLandlordAddress { get; set; }
        public string FebruaryLandlordAddress { get; set; }
        public string MarchLandlordAddress { get; set; }
        public List<Form80C> Form80CList { get; set; }
        public string DepartmentName { get; set; }
        public string GradeName { get; set; }
        public string RentProofFile { get; set; }
        public HttpPostedFileBase RentProofFileBase { get; set; }
        public decimal OldTaxProjection { get; set; }
        public decimal NewTaxProjection { get; set; }
        public string OldregimeOrNewregime { get; set; }
        public decimal TaxCollected { get; set; }

        public string FinancialYear { get; set; }
        #endregion

        public Form80CHeader()
        {
            Form80CList = new List<Form80C>();
        }
    }
}