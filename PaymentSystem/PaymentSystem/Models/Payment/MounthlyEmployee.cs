﻿
using static PaymentSystem.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Models.Payment
{
    public class MounthlyEmployee : Employee
    {
        public MounthlyEmployee(string id, IPaymentRepository repository) : base(id, repository)
        {
        }

        public void SetSalary(int amount)
        {
            var salary = new EmpSalary(this.Id, amount);
            _repository.AddSalary(salary);
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
    }
}
