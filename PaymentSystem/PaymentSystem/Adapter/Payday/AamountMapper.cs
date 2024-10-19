using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public static class AamountMapper
    {
        public static EmpSalaryCore ToCoreModel(AddEmpViewModel emp)
        {
            return new EmpSalaryCore
            {
                EmpId = emp.EmpId,
                Salary = emp.Amount,
            };
        }

        public static EmpSalaryCore ToCoreModel(ChgEmpViewModel emp)
        {
            return new EmpSalaryCore
            {
                EmpId = emp.EmpId,
                Salary = emp.Amount,
            };
        }


    }
}
