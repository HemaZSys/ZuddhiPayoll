namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Investment Declaration Form")]
    public class InvestmentDeclaration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Declaration_Id { get; set; }

        public int Declaration_master_id { get; set; }

        public int Declared_amt { get; set; }

        public int Proof_amount { get; set; }

        [StringLength(500)]
        public string Proof_doc_path { get; set; }

        [StringLength(500)]
        public string Proof_doc_name { get; set; }
    }
}
