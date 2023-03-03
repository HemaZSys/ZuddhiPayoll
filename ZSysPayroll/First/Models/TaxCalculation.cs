using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class TaxCalculation
    {
        public decimal deductionform80 { get; set; }
        public decimal earningform80 { get; set; }
        public decimal deductionpayslip { get; set; }
        public decimal earningpayslip { get; set; }

        public int empid { get; set; }
        public string empname { get; set; }
        public DateTime year { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxOutGo { get; set; }
        public decimal Educationcess { get; set; }
        public decimal Taxone { get; set; }
        public decimal Taxtwo { get; set; }
        public decimal Taxthree { get; set; }
        public decimal Taxfour { get; set; }
        public decimal Taxfive { get; set; }
        public decimal Taxsix { get; set; }
        public decimal Taxseven { get; set; }
        public decimal OldTaxone { get; set; }
        public decimal OldTaxtwo { get; set; }
        public decimal OldTaxthree { get; set; }
        public decimal OldTaxfour { get; set; }
        public decimal OldEducationcess { get; set; }
        public decimal OldTotalTax { get; set; }
        public decimal OldTaxOutGo { get; set; }

        public decimal AvancepaiIncometax { get; set; }

        public bool isOldRegimeSelected { get; set; }

        public bool isNewRegimeSelected { get; set; }

    }
}