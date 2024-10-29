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

        public IEnumerable<TimeCard> TimeCards => _repository.GetTimeCards(this.Id);

        public int HourlyRate { get; private set; }

        NextPaydayGetter _nextPaydayGetter = new NextFridayDateGetter();
        protected override NextPaydayGetter nextPaydayGetter => _nextPaydayGetter;

        protected override string EmployeeType => nameof(HourlyEmployee);

        public override void SetSalary(int amount, DateTime startDate)
        {
            this._repository.AddCompensationAlterEvent(this.Id, amount, startDate, nameof(HourlyEmployee));
        }


       

        public void AddTimeCard(TimeCard timeCard)
        {
            _repository.AddTimeCard(timeCard);
        }



        public override Payroll Settle()
        {
            return new Payroll
            {
                EmpId = this.Id,
                Salary = HourlyRate * TimeCards.Sum(x => x.Hours),
            };
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
