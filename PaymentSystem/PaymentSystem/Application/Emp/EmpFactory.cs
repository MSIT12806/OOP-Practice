using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public static class  EmpFactory
    {
        public static Models.Emp Build(string empId, string name, string address, Models.Emp.PayWayEnum payWay, IEmpRepository repository)
        {
            Models.Emp emp = null;
            switch (payWay)
            {
                case Models.Emp.PayWayEnum.Monthly:
                    emp = new Models.MounthlyEmployee(empId, repository);
                    break;

                case Models.Emp.PayWayEnum.Sales:
                    emp = new Models.SalesEmployee(empId, repository);
                    break;

                case Models.Emp.PayWayEnum.Union:
                    emp = new Models.UnionEmployee(empId, repository);
                    break;

                case Models.Emp.PayWayEnum.Hourly:
                default:
                    throw new NotImplementedException();
            }

            emp.InitialData(name, address);
            return emp;
        }
    }
}
