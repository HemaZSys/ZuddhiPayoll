using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First.Models
{
    public class ReportRow
    {
        public List<ReportField> ReportFieldList { get; set; }

        public ReportRow()
        {
            ReportFieldList = new List<ReportField>();
        }
    }
}