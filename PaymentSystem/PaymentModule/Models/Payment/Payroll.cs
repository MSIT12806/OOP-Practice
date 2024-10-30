using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class Payroll
    {
        public string EmpId { get; set; }
        public int TotoalPay =>  PayrollDetails.Sum(x => x.Amount);
        public IEnumerable<PayrollDetail> PayrollDetails { get; set; }
    }

    public class PayrollDetail
    {
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
