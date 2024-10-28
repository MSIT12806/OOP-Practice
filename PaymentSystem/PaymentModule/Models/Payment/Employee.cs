

using System;

namespace Payment.Models.Payment
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

        public abstract void AddCompensationAlterEvent(int amount, DateTime startDate);
        public abstract void AddPaymentEvent(DateTime payDate);
        protected abstract DateTime NextPayday(DateTime createDate);
    }
}
