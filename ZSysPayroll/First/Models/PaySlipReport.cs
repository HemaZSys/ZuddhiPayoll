using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class PaySlipReport
    {
        public List<ReportRow> ReportRowList { get; set; }

        public PaySlipReport()
        {
            ReportRowList = new List<ReportRow>();
        }
    }
}