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

        protected override string EmployeeType => throw new NotImplementedException();

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

        public override void SetSalary(int amount, DateTime startDate)
        {
            throw new NotImplementedException();
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
