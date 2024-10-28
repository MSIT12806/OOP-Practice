using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class SalesEmployee : MounthlyEmployee
    {
        public SalesEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<SalesReceipt> SalesReceipts => _repository.GetSalesReceipts(this.Id);

        public string AddSalesReceipt(string id, DateTime dateOnly, int commission)
        {
            var salesReceipt = new SalesReceipt
            {
                EmpId = id,
                SalesDate = dateOnly,
                Commission = commission
            };

            return this._repository.AddSalesReceipt(salesReceipt);
        }

        public IEnumerable<SalesReceipt> GetSalesReceipts()
        {
            return this._repository.GetSalesReceipts(this.Id);
        }

        public void DeleteSalesReceiptBy(string salesReceiptId)
        {
            this._repository.DeleteSalesReceiptBy(salesReceiptId);
        }

        public override Payroll Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payroll
            {
                EmpId = this.Id,
                Salary = salary.Amount + SalesReceipts.Sum(i => i.Commission),
            };
        }
    }
}
