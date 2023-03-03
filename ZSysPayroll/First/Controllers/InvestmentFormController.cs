using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using First.Models;

namespace First.Controllers
{
    public class InvestmentFormController : Controller
    {
        // GET: InvestmentForm       

        public ActionResult Index()
        {
            using (AppEntities1 db = new AppEntities1())
            {
                //InvestmentViewModel vm = new InvestmentViewModel();

                List<InvestmentDeclaration> InvestmentDeclaration = db.InvestmentDeclarations.ToList();
                List<InvestmentDeclarationMaster> InvestmentDeclarationMaster = db.InvestmentDeclarationMasters.ToList();

                var query = from master in InvestmentDeclarationMaster
                            join declaration in InvestmentDeclaration 
                            on master.Declaration_master_id equals declaration.Declaration_master_id
                             into decform from declaration in decform.DefaultIfEmpty()
                            select new Investmentview
                            {
                                // proofamount=subdec.Proof_amount,
                                Proof_amount = declaration.Proof_amount,
                                Declaration_category = master.Declaration_category,
                                Declaration_master_id = master.Declaration_master_id,
                                Declaration_type = master.Declaration_type

                                //InvestmentDeclarationMaster = master,
                                //InvestmentDeclaration = declaration

                                //id = master != null ? master.Declaration_master_id : declaration.Declaration_master_id,
                                //declarationcategory = master != null ? master.Declaration_category : string.Empty,
                                //declarationtype = master != null ? master.Declaration_type : string.Empty,
                                //proofamount= declaration.Proof_amount,
                                //decamount = declaration.Declared_amt
                                //InvestmentDeclaration = subdec,
                                //InvestmentDeclarationMaster = master
                            };
                return View(query);

                //var formRecord = from c in InvestmentDeclaration
                //                 join d in InvestmentDeclarationMaster on c.Declaration_master_id equals d.Declaration_master_id into table1
                //                     from d in table1.ToList()
                //                         //join i in incentives on e.Incentive_Id equals i.IncentiveId into table2
                //                         //from i in table2.ToList()
                //                     select new InvestmentViewModel
                //                     {
                //                         InvestmentDeclaration = c,
                //                         InvestmentDeclarationMaster = d

                //                     };
                
            }
        }
    }
}