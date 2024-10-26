namespace PaymentSystem.Models
{
    public class MounthlyEmployee : Emp
    {
        public MounthlyEmployee(string id, IEmpRepository repository) : base(id, repository)
        {
        }

        public void SetSalary(int amount)
        {
            var salary = new EmpSalaryCore(this.Id, amount, PayWayEnum.Monthly);
            _repository.AddSalary(salary);
        }

        public EmpSalaryCore GetSalary()
        {
            return _repository.GetSalary(this.Id);
        }

        public override Payment Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payment
            {
                EmpId = this.Id,
                Salary = salary.Amount,
            };
        }
    }
}
