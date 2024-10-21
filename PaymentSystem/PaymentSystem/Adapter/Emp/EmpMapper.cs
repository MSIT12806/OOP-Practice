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

        public static ChgEmpViewModel ToChgModel(EmpCore emp)
        {
            return new ChgEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public static EmpCore ToCoreModel(AddEmpViewModel addEmp)
        {
            var core = new EmpCore(addEmp.EmpId);
            core.InjectData(addEmp.Name, addEmp.Address);
            return core;
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

        public static EmpQueryViewModel ToInfoModel(EmpCore core)
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
