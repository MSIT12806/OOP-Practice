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

        public override string EmployeeType => nameof(UnionEmployee);

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

        public override void SetSalary(int amount, DateTime startDate)
        {
            var salary = new EmpSalary(this.Id, amount, nameof(MounthlyEmployee));
            _repository.AddCompensationAlterEvent(this.Id, amount, startDate, nameof(UnionEmployee));
        }

        public override Payroll Settle(DateTime payday)
        {
            var salary = this.GetSalary();
            var serviceCharges = this.GetServiceCharge();
            var totalServiceCharge = serviceCharges.Sum(x => x.Amount);
            var payrollDetail = new PayrollDetail
            {
                Description = "Regular Pay",
                Amount = salary,
            };

            var serviceChargePayrollDetail = new PayrollDetail
            {
                Description = "Service Charge",
                Amount = -totalServiceCharge,
            };

            return new Payroll
            {
                EmpId = this.Id,
                PayrollDetails = new[] { payrollDetail },
            };
        }

        public override int GetSalary()
        {
            return _repository.GetSalary(this.Id).Amount;
        }
    }
}
