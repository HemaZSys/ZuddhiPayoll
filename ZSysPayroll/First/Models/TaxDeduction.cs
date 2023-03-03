namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaxDeduction")]
    public partial class TaxDeduction
    {
        [Key]
        public int TaxId { get; set; }

        [Column("80C_Id")]
        public int? C80C_Id { get; set; }
    }
}
