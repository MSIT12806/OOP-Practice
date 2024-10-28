using Payment.Models.Payment;
using System;

namespace Payment.Application.Payment
{
    public static class EmpFactory
    {
        public static Employee Build(string empId, Type EmployeeType, IPaymentRepository repository)
        {
            Employee emp = null;
            switch (EmployeeType.Name)
            {
                case nameof(MounthlyEmployee):
                    emp = new MounthlyEmployee(empId, repository);
                    break;

                case nameof(SalesEmployee):
                    emp = new SalesEmployee(empId, repository);
                    break;

                case nameof(UnionEmployee):
                    emp = new UnionEmployee(empId, repository);
                    break;

                case nameof(HourlyEmployee):
                    emp = new HourlyEmployee(empId, repository);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return emp;
        }
    }
}
