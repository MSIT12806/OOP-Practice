
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


    }
}
