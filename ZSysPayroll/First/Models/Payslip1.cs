namespace First.Models
    {
        using System;
        using System.Collections.Generic;
        using System.ComponentModel.DataAnnotations;
        using System.ComponentModel.DataAnnotations.Schema;
        using System.Data.Entity.Spatial;

        [Table("Payslip1")]
        public partial class Payslip1
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int PId { get; set; }

            [Required]
            [StringLength(50)]
            public string Basic { get; set; }

            [Required]
            [StringLength(50)]
            public string HRA { get; set; } 

            [StringLength(50)]
            public string PF { get; set; }

            [StringLength(50)]
            public string Incometax { get; set; }

            [StringLength(50)]
            public string Professiontax { get; set; }

            [StringLength(50)]
            public string SalaryAdv { get; set; }

            [StringLength(50)]
            public string Total_sal { get; set; }

            [Column(TypeName = "date")]
            public DateTime? Month { get; set; }  

            [StringLength(50)]
            public string Gross_Total { get; set; }           

            [StringLength(50)]
            public string Special_allowance { get; set; }

            [StringLength(50)]
            public string Gradutity { get; set; }

            [StringLength(50)]
            public string Telephone { get; set; }

            [StringLength(50)]
            public string Books_Periodicals { get; set; }

            [StringLength(50)]
            public string Food_coupon { get; set; }

            [StringLength(50)]
            public string LTA { get; set; }

            [StringLength(50)]
            public string Variable_pay { get; set; }

            [Column(TypeName = "date")]
            public DateTime? Year { get; set; }
        }
    }