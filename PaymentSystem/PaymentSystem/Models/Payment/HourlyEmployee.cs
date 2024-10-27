namespace PaymentSystem.Models.Payment
{
    public class HourlyEmployee : Employee
    {
        public HourlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<TimeCard> TimeCards => _repository.GetTimeCards(this.Id);

        public int HourlyRate { get; private set; }

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
