using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;
using PaymentSystem.Models.BasicDataMaintenece;

namespace PaymentSystem.Adapter.BasicDataMaintenence
{
    public static class EmpMapper
    {
        public static AddEmpViewModel ToAddModel(Employee emp)
        {
            return new AddEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public static ChgEmpViewModel ToChgModel(Employee emp)
        {
            return new ChgEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public static EmpQueryViewModel ToQueryModel(Employee core)
        {
            return new EmpQueryViewModel
            {
                EmpId = core.Id,
                Name = core.Name,
                Address = core.Address
            };
        }
    }
}
