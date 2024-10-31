using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class HourlyEmployee : Employee
    {
        public HourlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<TimeCard> TimeCards => this._repository.GetTimeCards(this.Id);

        public int HourlyRate => this.GetSalary();

        NextPaydayGetter _nextPaydayGetter = new NextFridayDateGetter();
        protected override NextPaydayGetter nextPaydayGetter => this._nextPaydayGetter;

        public override string EmployeeType => nameof(HourlyEmployee);

        public override void SetSalary(int amount, DateTime startDate)
        {
            this._repository.AddCompensationAlterEvent(this.Id, amount, startDate, nameof(HourlyEmployee));
        }
        public override int GetSalary()
        {
            return this._repository.GetSalary(this.Id).Amount;
        }

        public void AddTimeCard(DateTime wordDate, int hours)
        {
            var timeCard = new TimeCard(this.Id, wordDate, hours);
            this._repository.AddTimeCard(timeCard);
        }



        public override Payroll Settle(DateTime payday)
        {
            const int OVERTIME_RATE = 2;
            var previousPayday = this.GetPayday(payday) ?? DateTime.MinValue;

            var regularPayrollDetail = new PayrollDetail
            {
                Description = "Regular Pay",
                Amount = this.HourlyRate * this.TimeCards.Where(x => x.WorkDate > previousPayday).Sum(x => x.GetRegularHours()),
            };

            var overtimePayrollDetail = new PayrollDetail
            {
                Description = "Overtime Pay",
                Amount = this.HourlyRate * OVERTIME_RATE * this.TimeCards.Where(x => x.WorkDate > previousPayday).Sum(x => x.GetOvertimeHours()),
            };

            var payroll = new Payroll
            {
                EmpId = this.Id,
                PayrollDetails = new List<PayrollDetail> { regularPayrollDetail, overtimePayrollDetail },
            };

            base.AddPayroll(payroll);
            return payroll;
        }
    }


    public class NextFridayDateGetter : NextPaydayGetter
    {
        public override DateTime GetNextPayday(DateTime createDate)
        {
            return GetNextWeekFridayDate(createDate);
        }
        private static DateTime GetNextWeekFridayDate(DateTime createDate)
        {
            switch (createDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return createDate.AddDays(5);
                case DayOfWeek.Monday:
                    return createDate.AddDays(4);
                case DayOfWeek.Tuesday:
                    return createDate.AddDays(3);
                case DayOfWeek.Wednesday:
                    return createDate.AddDays(2);
                case DayOfWeek.Thursday:
                    return createDate.AddDays(1);
                case DayOfWeek.Friday:
                    return createDate.AddDays(7);
                case DayOfWeek.Saturday:
                    return createDate.AddDays(6);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
