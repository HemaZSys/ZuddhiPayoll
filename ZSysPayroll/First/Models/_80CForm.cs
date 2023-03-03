using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class _TaxDeclForm
    {
        public int Id { get; set; }
        public int TaxId { get; set; }
        public int Lifeinsure_DAmt { get; set; }

        public int Lifeinsure_PAmt { get; set; }

        public HttpPostedFileBase Lifeinsure_Pfile { get; set; }
        public int PPF_DAmt { get; set; }

        public int PPF_PAmt { get; set; }

        public HttpPostedFileBase PPF_Pfile { get; set; }
        public int ULIP_DAmt { get; set; }

        public int ULIP_PAmt { get; set; }

        public HttpPostedFileBase ULIP_Pfile { get; set; }
        public int InvestMutual_DAmt { get; set; }

        public int InvestMutual_PAmt { get; set; }

        public HttpPostedFileBase InvestMutual_Pfile { get; set; }
        public int InvestEquity_DAmt { get; set; }

        public int InvestEquity_PAmt { get; set; }

        public HttpPostedFileBase InvestEquity_Pfile { get; set; }

        public DateTime NSC16_Date { get; set; }

        public int NSC16_DAmt { get; set; }

        public int NSC16_PAmt { get; set; }

        public HttpPostedFileBase NSC16_Pfile { get; set; }
        public DateTime NSC17_Date { get; set; }

        public int NSC17_DAmt { get; set; }

        public int NSC17_PAmt { get; set; }

        public HttpPostedFileBase NSC17_Pfile { get; set; }
        public DateTime NSC18_Date { get; set; }

        public int NSC18_DAmt { get; set; }

        public int NSC18_PAmt { get; set; }

        public HttpPostedFileBase NSC18_Pfile { get; set; }
        public int NSC19_DAmt { get; set; }

        public int NSC19_PAmt { get; set; }

        public HttpPostedFileBase NSC19_Pfile { get; set; }
        public int NSC20_DAmt { get; set; }

        public int NSC20_PAmt { get; set; }

        public HttpPostedFileBase NSC20_Pfile { get; set; }
        public int NSC21_DAmt { get; set; }

        public int NSC21_PAmt { get; set; }

        public HttpPostedFileBase NSC21_Pfile { get; set; }
        public int RepaymentHouseloan_DAmt { get; set; }

        public int RepaymentHouseloan_PAmt { get; set; }

        public HttpPostedFileBase RepaymentHouseloan_Pfile { get; set; }
        public int PensionPlan80CCC_DAmt { get; set; }

        public int PensionPlan80CCC_PAmt { get; set; }

        public HttpPostedFileBase PensionPlan80CCC_Pfile { get; set; }
        public int Tutionfeesfor2child_DAmt { get; set; }

        public int Tutionfessfor2child_PAmt { get; set; }

        public HttpPostedFileBase Tutionfessfor2child_Pfile { get; set; }

        public int FDQualityfor80C_DAmt { get; set; }

        public int FDQualityfor80C_PAmt { get; set; }

        public HttpPostedFileBase FDQualityfor80C_Pfile { get; set; }
        public int Postoffctimedeposit_DAmt { get; set; }

        public int Postoffctimedeposit_PAmt { get; set; }

        public HttpPostedFileBase Postoffctimedeposit_Pfile { get; set; }
        public int Sukanyasamriddhi_DAmt { get; set; }

        public int Sukanyasamriddhi_PAmt { get; set; }

        public HttpPostedFileBase Sukanyasamriddhi_Pfile { get; set; }

        public int Infrabond_DAmt { get; set; }

        public int Infrabond_PAmt { get; set; }


        public int Tax80CCD1_DAmt { get; set; }

        public int Tax80CCD1_PAmt { get; set; }

        public HttpPostedFileBase Tax80CCD1_Pfile { get; set; }


    }
}