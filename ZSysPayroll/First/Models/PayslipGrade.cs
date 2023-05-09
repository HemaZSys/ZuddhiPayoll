namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayslipGrade")]
    public partial class PayslipGrade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(300)]
        public string GradeName { get; set; }        
        public int LineNumber { get; set; }
       
        public decimal SequenceNumber { get; set; }
        
        public int Section { get; set; }

        public decimal Percentage { get; set; }

        public decimal fixedAmount { get; set; }

        [StringLength(300)]
        public string SectionDescription { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int ControlType { get; set; }
        public int EarningOrDeduction { get; set; }
      
    }
}
