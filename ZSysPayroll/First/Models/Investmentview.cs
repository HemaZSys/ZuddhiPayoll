using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class Investmentview
    {
        
        public int Declaration_Id { get; set; }

        public int Declaration_master_id { get; set; }

        public int Declared_amt { get; set; }

        public int Proof_amount { get; set; }
       
        public string Proof_doc_path { get; set; }
        
        public string Proof_doc_name { get; set; }      
        
        public string Declaration_category { get; set; }

        
        public string Declaration_type { get; set; }

        public int Is_active { get; set; }

        public int Sort_order { get; set; }

        public List<Investmentview> investmentviews { get; set; }

    }
}