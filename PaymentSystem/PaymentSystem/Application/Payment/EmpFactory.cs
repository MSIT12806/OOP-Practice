using PaymentSystem.Models;
using PaymentSystem.Models.Payment;
using static PaymentSystem.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Application.Payment
{
    public static class  EmpFactory
    {
        public static Employee Build(string empId, PayWayEnum payWay, IPaymentRepository repository)
        {
           Employee emp = null;
            switch (payWay)
            {
                case PayWayEnum.Monthly:
                    emp = new MounthlyEmployee(empId, repository);
                    break;

                case PayWayEnum.Sales:
                    emp = new SalesEmployee(empId, repository);
                    break;

                case PayWayEnum.Union:
                    emp = new UnionEmployee(empId, repository);
                    break;

                case PayWayEnum.Hourly:
                default:
                    throw new NotImplementedException();
            }

            return emp;
        }
    }
}
