using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class Payroll
    {
        public string EmpId { get; set; }
        public DateTime PayDate { get; set; }
        public int TotoalPay => this.PayrollDetails.Sum(x => x.Amount);
        public IEnumerable<PayrollDetail> PayrollDetails { get; set; }
    }

    public class PayrollDetail
    {
        public string PayrollId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
