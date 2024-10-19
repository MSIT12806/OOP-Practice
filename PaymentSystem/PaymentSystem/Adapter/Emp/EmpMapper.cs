using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public static class EmpMapper
    {
        public static AddEmpViewModel ToAddModel(EmpCore emp)
        {
            return new AddEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public static ChgEmpViewModel ToChgModel(EmpCore emp, EmpSalaryCore amount)
        {
            return new ChgEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address,
                Amount = amount.Salary
            };
        }

        public static EmpCore ToCoreModel(AddEmpViewModel addEmp)
        {
            return new EmpCore
            {
                Id = addEmp.EmpId,
                Name = addEmp.Name,
                Address = addEmp.Address
            };
        }

        public static EmpCore ToCoreModel(ChgEmpViewModel chgEmp)
        {
            return new EmpCore
            {
                Id = chgEmp.EmpId,
                Name = chgEmp.Name,
                Address = chgEmp.Address
            };
        }
    }
}
