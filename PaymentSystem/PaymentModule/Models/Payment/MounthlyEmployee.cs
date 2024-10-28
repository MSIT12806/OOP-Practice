

using System;

namespace Payment.Models.Payment
{
    public class MounthlyEmployee : Employee
    {
        public MounthlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public void SetSalary(int amount, DateTime dateTime)
        {
            var salary = new EmpSalary(this.Id, amount);
            _repository.AddCompensationAlterEvent(this.Id, amount, dateTime, nameof(MounthlyEmployee));
        }

        public EmpSalary GetSalary()
        {
            return _repository.GetSalary(this.Id);
        }

        public override Payroll Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payroll
            {
                EmpId = this.Id,
                Salary = salary.Amount,
            };
        }

        public override void AddCompensationAlterEvent(int amount, DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public override void AddPaymentEvent(DateTime payDate)
        {
            throw new NotImplementedException();
        }

        protected override DateTime NextPayday(DateTime createDate)
        {
            throw new NotImplementedException();
        }
    }
}
