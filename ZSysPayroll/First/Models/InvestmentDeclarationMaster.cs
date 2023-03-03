namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("investment declaration master")]
    public class InvestmentDeclarationMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Declaration_master_id { get; set; }

        [StringLength(50)]
        public string Declaration_category { get; set; }

        [StringLength(50)]
        public string Declaration_type { get; set; }
        
        public int Is_active { get; set; }
        
        public int Sort_order { get; set; }
    }
}
