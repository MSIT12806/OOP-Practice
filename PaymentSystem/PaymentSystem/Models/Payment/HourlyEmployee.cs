
using static PaymentSystem.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Models.Payment
{
    public class HourlyEmployee : Employee
    {
        public HourlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<TimeCard> TimeCards => _repository.GetTimeCards(this.Id);

        public int HourlyRate { get; private set; }

        public override void AddCompensationAlterEvent(int amount, DateOnly startDate)
        {
            this._repository.AddCompensationAlterEvent(this.Id, amount, startDate, PayWayEnum.Hourly);
        }

        public override void AddPaymentEvent(DateOnly createDate)
        {
            this._repository.AddPaymentEvent(this.Id, NextPayday(createDate), PayWayEnum.Hourly);
        }
        protected override DateOnly NextPayday(DateOnly createDate)
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
}
