using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public static class EmpMapper
    {
        public static AddEmpViewModel ToAddModel(Emp emp)
        {
            return new AddEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public static ChgEmpViewModel ToChgModel(Emp emp)
        {
            return new ChgEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        //public static EmpCore ToCoreModel(AddEmpViewModel addEmp)
        //{
        //    var core = new EmpCore(addEmp.EmpId);
        //    core.InjectData(addEmp.Name, addEmp.Address);
        //    return core;
        //}

        //public static EmpCore ToCoreModel(ChgEmpViewModel chgEmp)
        //{
        //    var core = new EmpCore(chgEmp.EmpId);
        //    core.InjectData(chgEmp.Name, chgEmp.Address);
        //    return core;
        //}

        public static EmpQueryViewModel ToQueryModel(Emp core)
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
