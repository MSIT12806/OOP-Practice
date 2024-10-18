using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public static class AamountMapper
    {
        public static AmountCore ToCoreModel(AddEmpViewModel addEmp)
        {
            return new AmountCore
            {
                EmpId = addEmp.EmpId,
                Amount = addEmp.Amount,
            };
        }
    }
}
