using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace First.Models
{
    public class _TaxDeclFormSetting
    {
        #region Instance Properties

        public int Id { get; set; }

        [Display(Name = "Line #")]
        public string LineNumber { get; set; }

        [Display(Name = "Seq #")]
        public decimal SequenceNumber { get; set; }

        public string Section { get; set; }

        public string SectionName { get; set; }

        public string Description { get; set; }

        public int? ControlType { get; set; }

        [Display(Name = "Max Val")]
        public decimal? MaximumValue { get; set; }

        [Display(Name = "Attachment")]
        public bool? AttachmentRequired { get; set; }

        [Display(Name = "Max Size")]
        public string AttachmentSize { get; set; }

        [Display(Name = "File Type")]
        public string AttachmentType { get; set; }

        [Display(Name = "Earning/ Deduction")]
        public int EarningOrDeduction { get; set; }
        #endregion Instance Properties
    }
}