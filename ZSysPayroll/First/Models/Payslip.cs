namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payslip")]
    public partial class Payslip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PId { get; set; }

        public int EmployeeId { get; set; }       

        [Required]
        [StringLength(50)]
        public string Basic { get; set; }

        [Required]
        [StringLength(50)]
        public string HRA { get; set; }

        [Required]
        [StringLength(50)]
        public string DA { get; set; }

        [StringLength(50)]
        public string Coveyance { get; set; }

        [StringLength(50)]
        public string Bonus { get; set; }        

        [StringLength(50)]
        public string PF { get; set; }

        [StringLength(50)]
        public string Incometax { get; set; }

        [StringLength(50)]
        public string Professiontax { get; set; }

        [StringLength(50)]
        public string SalaryAdv { get; set; }

        [StringLength(50)]
        public string Others { get; set; }

        [StringLength(50)]
        public string Total_sal { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Month { get; set; }
        
        public string Gross_Total { get; set; }

        [StringLength(50)]
        public string Deduction_Total { get; set; }         

        
    }
}
