

namespace PaymentSystem.Models.Payment
{
    public abstract class Employee
    {
        protected IPaymentRepository _repository;
        public string Id { get; }


        public Employee(string id, IPaymentRepository repository)
        {
            this.Id = id;
            this._repository = repository;
        }

        public abstract Payroll Settle();

        public abstract void AddCompensationAlterEvent(int amount, DateOnly startDate);
        public abstract void AddPaymentEvent(DateOnly payDate);
        protected abstract DateOnly NextPayday(DateOnly createDate);
    }
}
