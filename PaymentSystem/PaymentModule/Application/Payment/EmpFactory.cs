using Payment.Models.Payment;
using System;

namespace Payment.Application.Payment
{
    public static class EmpFactory
    {
        public static Employee Build(string empId, string employeeType, IPaymentRepository repository)
        {
            switch (employeeType)
            {
                case nameof(MounthlyEmployee):
                    return new MounthlyEmployee(empId, repository);

                case nameof(SalesEmployee):
                    return new SalesEmployee(empId, repository);

                case nameof(UnionEmployee):
                    return new UnionEmployee(empId, repository);

                case nameof(HourlyEmployee):
                    return new HourlyEmployee(empId, repository);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
