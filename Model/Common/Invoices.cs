using System;
using System.Collections.Generic;

namespace CrudReportGenerate.Model.Common
{
    public class Invoice
    {
        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceAmount { get; set; }
        public DateTime PaymentDueDate { get; set; }

        //Payment Repositary

        public int PaymentAmount { get; set; }
    }
}
