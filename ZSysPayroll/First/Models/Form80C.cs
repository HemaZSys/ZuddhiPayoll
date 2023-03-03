using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class Form80C
    {
        #region Instance Properties

        public int Id { get; set; }

        public int Form80CsettingId { get; set; }

        public string LineNumber { get; set; }

        public string EmployeeId { get; set; }
        public decimal? SequenceNumber { get; set; }

        public string Section { get; set; }
        public string SectionName{ get; set; }

        public string Description { get; set; }
        public bool Descriptioncheckbox { get; set; }

        public string DescriptionLabel { get; set; }

        public int TaxId { get; set; }
        public decimal Declared_Amt { get; set; }

        public decimal Proof_Amt { get; set; }

        public HttpPostedFileBase Proof_file { get; set; }

        public String Proof_file_location { get; set; }

        public DateTime MonthYear { get; set; }

        #endregion Instance Properties
    }
}