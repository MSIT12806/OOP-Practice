using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class SalesEmployee : Employee
    {
        public SalesEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<SalesReceipt> SalesReceipts => _repository.GetSalesReceipts(this.Id);

        NextPaydayGetter _nextPaydayGetter = new NextTwoWeeksFridayGetter();
        protected override NextPaydayGetter nextPaydayGetter => _nextPaydayGetter;

        public override string EmployeeType => nameof(SalesEmployee);

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

        public override void SetSalary(int amount, DateTime startDate)
        {
            this._repository.AddCompensationAlterEvent(this.Id, amount, startDate, nameof(SalesEmployee));
        }

        public override Payroll Settle(DateTime payday)
        {
            var salesReceipts = this.GetSalesReceipts();
            var salary = this.GetSalary();
            var commission = salesReceipts.Sum(x => x.Commission);
            var payrollDetail = new PayrollDetail
            {
                Description = "Regular Pay",
                Amount = salary,
            };

            var commissionPayrollDetail = new PayrollDetail
            {
                Description = "Commission",
                Amount = commission,
            };

            return new Payroll
            {
                EmpId = this.Id,
                PayrollDetails = new[] { payrollDetail, commissionPayrollDetail },
            };
        }

        public override int GetSalary()
        {
            return this._repository.GetSalary(this.Id).Amount;
        }
    }

    public class NextTwoWeeksFridayGetter: NextPaydayGetter
    {
        public override DateTime GetNextPayday(DateTime createDate)
        {
            return GetNextTwoWeeksFridayDate(createDate);
        }

        private static DateTime GetNextTwoWeeksFridayDate(DateTime createDate)
        {
            switch (createDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return createDate.AddDays(12);
                case DayOfWeek.Monday:
                    return createDate.AddDays(11);
                case DayOfWeek.Tuesday:
                    return createDate.AddDays(10);
                case DayOfWeek.Wednesday:
                    return createDate.AddDays(9);
                case DayOfWeek.Thursday:
                    return createDate.AddDays(8);
                case DayOfWeek.Friday:
                    return createDate.AddDays(14);
                case DayOfWeek.Saturday:
                    return createDate.AddDays(13);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
