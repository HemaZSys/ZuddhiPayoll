namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grade")]
    public partial class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Grade_Id { get; set; }

        [StringLength(50)]
        public string Grade_Name { get; set; }

        public int? Emp_Id { get; set; }

        public int? Pid { get; set; }
    }
}
