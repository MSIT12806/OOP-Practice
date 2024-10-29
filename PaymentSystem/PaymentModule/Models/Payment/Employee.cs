

using System;

namespace Payment.Models.Payment
{
    public abstract class Employee
    {
        protected IPaymentRepository _repository;
        protected abstract NextPaydayGetter nextPaydayGetter { get; }
        public string Id { get; }
        public abstract string EmployeeType { get; }

        public Employee(string id, IPaymentRepository repository)
        {
            this.Id = id;
            this._repository = repository;
        }

        public abstract Payroll Settle();
        public abstract void SetSalary(int amount, DateTime startDate);
        public abstract int GetSalary();
        public void AddPaymentEvent(DateTime payDate)
        {
            this._repository.AddPaymentEvent(this.Id, NextPayday(payDate), EmployeeType);
        }
        public DateTime NextPayday(DateTime createDate)
        {
            return nextPaydayGetter.GetNextPayday(createDate);
        }
    }

    public abstract class NextPaydayGetter
    {
        public abstract DateTime GetNextPayday(DateTime createDate);
    }
}
