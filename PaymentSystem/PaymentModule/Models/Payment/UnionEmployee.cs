using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Models.Payment
{
    public class UnionEmployee : Employee
    {
        public UnionEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<ServiceCharge> ServiceCharges => _repository.GetServiceCharges(this.Id);

        NextPaydayGetter _nextPaydayGetter = new MounthlyPaymentDateGetter();
        protected override NextPaydayGetter nextPaydayGetter => _nextPaydayGetter;

        protected override string EmployeeType => nameof(UnionEmployee);

        public string SubmitServiceCharge(int amount, DateTime date)
        {
            var serviceCharge = new ServiceCharge
            {
                EmpId = this.Id,
                Amount = amount,
                ApplyDate = date,
            };

            return _repository.AddServiceCharge(serviceCharge);
        }

        public ServiceCharge GetServiceChargeBy(string setviceChargeId)
        {
            var db = _repository.GetServiceCharges(this.Id).FirstOrDefault(x => x.Id == setviceChargeId);
            return db;
        }

        public void WithdrawServiceCharge(string setviceChargeId)
        {
            _repository.DeleteServiceChargeBy(setviceChargeId);
        }

        public IEnumerable<ServiceCharge> GetServiceCharge()
        {
            return _repository.GetServiceCharges(this.Id);
        }

        public override Payroll Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payroll
            {
                EmpId = this.Id,
                Salary = salary.Amount - ServiceCharges.Sum(i => i.Amount),
            };
        }

        public override void SetSalary(int amount, DateTime startDate)
        {
            var salary = new EmpSalary(this.Id, amount, nameof(MounthlyEmployee));
            _repository.AddCompensationAlterEvent(this.Id, amount, startDate, nameof(UnionEmployee));
        }
    }
}
