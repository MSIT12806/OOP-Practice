using PaymentSystem.Application.Emp;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public class EmpMapper 
    {
        public AddEmpViewModel ToAddModel(EmpCore emp)
        {
            return new AddEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public ChgEmpViewModel ToChgModel(EmpCore emp)
        {
            return new ChgEmpViewModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }

        public EmpCore ToCoreModel(AddEmpViewModel addEmp)
        {
            return new EmpCore
            {
                Id = addEmp.EmpId,
                Name = addEmp.Name,
                Address = addEmp.Address
            };
        }

        public EmpCore ToCoreModel(ChgEmpViewModel chgEmp)
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
