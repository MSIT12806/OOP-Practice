namespace PaymentSystem.Models.Payment
{
    public class UnionEmployee : MounthlyEmployee
    {
        public UnionEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<ServiceChargeCore> ServiceCharges => _repository.GetServiceCharges(this.Id);

        public string SubmitServiceCharge(string id, int amount, DateOnly dateOnly)
        {
            var serviceCharge = new ServiceChargeCore
            {
                EmpId = id,
                Amount = amount,
                ApplyDate = dateOnly,
            };

            return _repository.AddServiceCharge(serviceCharge);
        }

        public ServiceChargeCore GetServiceChargeBy(string setviceChargeId)
        {
            var db = _repository.GetServiceCharges(this.Id).FirstOrDefault(x => x.Id == setviceChargeId);
            return db;
        }

        public void WithdrawServiceCharge(string setviceChargeId)
        {
            _repository.DeleteServiceChargeBy(setviceChargeId);
        }

        public IEnumerable<ServiceChargeCore> GetServiceCharge()
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
    }
}
