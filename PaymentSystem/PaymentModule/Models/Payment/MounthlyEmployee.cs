﻿

using System;

namespace Payment.Models.Payment
{
    public class MounthlyEmployee : Employee
    {
        protected NextPaydayGetter _nextPaydayGetter = new MounthlyPaymentDateGetter();
        protected override NextPaydayGetter nextPaydayGetter => _nextPaydayGetter;
        public override string EmployeeType => nameof(MounthlyEmployee);

        public MounthlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public override void SetSalary(int amount, DateTime dateTime)
        {
            var salary = new EmpSalary(this.Id, amount, nameof(MounthlyEmployee));
            _repository.AddCompensationAlterEvent(this.Id, amount, dateTime, nameof(MounthlyEmployee));
        }

        public override int GetSalary()
        {
            return _repository.GetSalary(this.Id).Amount;
        }

        public override Payroll Settle(DateTime dateTime)
        {
            var salary =this.GetSalary();
            var payrollDetail = new PayrollDetail
            {
                Description = "Regular Pay",
                Amount = salary,
            };

            return new Payroll
            {
                EmpId = this.Id,
                PayrollDetails = new[] { payrollDetail },
            };
        }
    }


    public class MounthlyPaymentDateGetter : NextPaydayGetter
    {
        public override DateTime GetNextPayday(DateTime createDate)
        {
            return GetNextMonthLastDay(createDate);
        }

        private DateTime GetNextMonthLastDay(DateTime payDate)
        {
            var nextDay = payDate.AddDays(1);
            var lastDayOfNextDayMounth = new DateTime(nextDay.Year, nextDay.Month, DateTime.DaysInMonth(nextDay.Year, nextDay.Month));
            return lastDayOfNextDayMounth;
        }
    }
}
