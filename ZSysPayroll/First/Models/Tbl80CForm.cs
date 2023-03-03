namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;

    public partial class TblTaxDeclForm
    {
        [Key]
        [Column("80C_Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C80C_Id { get; set; }

        public int? TaxId { get; set; }

        public int? Lifeinsure_DAmt { get; set; }

        public int? Lifeinsure_PAmt { get; set; }

        public byte[]  Lifeinsure_img { get; set; }

        [StringLength(300)]
        public string Lifeinsure_Pfile { get; set; }

        public int? PPF_DAmt { get; set; }

        public int? PPF_PAmt { get; set; }

        [StringLength(300)]
        public string PPF_Pfile { get; set; }

        public int? ULIP_DAmt { get; set; }

        public int? ULIP_PAmt { get; set; }

        [StringLength(300)]
        public string ULIP_Pfile { get; set; }

        public int? InvestMutual_DAmt { get; set; }

        public int? InvestMutual_PAmt { get; set; }

        [StringLength(300)]
        public string InvestMutual_Pfile { get; set; }

        public int? InvestEquity_DAmt { get; set; }

        public int? InvestEquity_PAmt { get; set; }

        [StringLength(300)]
        public string InvestEquity_Pfile { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NSC16_Date { get; set; }

        public int? NSC16_DAmt { get; set; }

        public int? NSC16_PAmt { get; set; }

        [StringLength(300)]
        public string NSC16_Pfile { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NSC17_Date { get; set; }

        public int? NSC17_DAmt { get; set; }

        public int? NSC17_PAmt { get; set; }

        [StringLength(300)]
        public string NSC17_Pfile { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NSC18_Date { get; set; }

        public int? NSC18_DAmt { get; set; }

        public int? NSC18_PAmt { get; set; }

        [StringLength(300)]
        public string NSC18_Pfile { get; set; }

        public int? NSC19_DAmt { get; set; }

        public int? NSC19_PAmt { get; set; }

        [StringLength(300)]
        public string NSC19_Pfile { get; set; }

        public int? NSC20_DAmt { get; set; }

        public int? NSC20_PAmt { get; set; }

        [StringLength(300)]
        public string NSC20_Pfile { get; set; }

        public int? NSC21_DAmt { get; set; }

        public int? NSC21_PAmt { get; set; }

        [StringLength(300)]
        public string NSC21_Pfile { get; set; }

        public int? RepaymentHouseloan_DAmt { get; set; }

        public int? RepaymentHouseloan_PAmt { get; set; }

        [StringLength(300)]
        public string RepaymentHouseloan_Pfile { get; set; }

        public int? PensionPlan80CCC_DAmt { get; set; }

        public int? PensionPlan80CCC_PAmt { get; set; }

        [StringLength(300)]
        public string PensionPlan80CCC_Pfile { get; set; }

        public int? Tutionfeesfor2child_DAmt { get; set; }

        public int? Tutionfeesfor2child_PAmt { get; set; }

        [StringLength(300)]
        public string Tutionfeesfor2child_Pfile { get; set; }

        public int? FDQualifyfor80C_DAmt { get; set; }

        public int? FDQualifyfor80C_PAmt { get; set; }

        [StringLength(300)]
        public string FDQualifyfor80C_Pfile { get; set; }

        public int? Postoffctimedeposit_DAmt { get; set; }

        public int? Postoffctimedeposit_PAmt { get; set; }

        [StringLength(300)]
        public string Postoffctimedeposit_Pfile { get; set; }

        public int? Sukanyasamriddhi_DAmt { get; set; }

        public int? Sukanyasamriddhi_PAmt { get; set; }

        [StringLength(300)]
        public string Sukanyasamriddhi_Pfile { get; set; }

        public int? Infrabond_DAmt { get; set; }

        public int? Infrabond_PAmt { get; set; }

        public int? Tax80CCD1_DAmt { get; set; }

        public int? Tax80CCD1_PAmt { get; set; }

        [StringLength(300)]
        public string Tax80CCD1_Pfile { get; set; }
    }
}
