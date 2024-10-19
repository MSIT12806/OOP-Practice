using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public static class AamountMapper
    {
        public static EmpSalaryCore ToCoreModel(AddEmpViewModel addEmp)
        {
            return new EmpSalaryCore
            {
                EmpId = addEmp.EmpId,
                Salary = addEmp.Amount,
            };
        }
    }
}
