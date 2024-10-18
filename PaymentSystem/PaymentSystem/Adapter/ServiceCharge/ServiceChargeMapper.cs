using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public static class ServiceChargeMapper
    {
        public static ServiceChargeViewModel ToViewModel(ServiceChargeCore serviceChargeCore)
        {
            return new ServiceChargeViewModel
            {
                EmpId = serviceChargeCore.EmpId,
                MemberId = serviceChargeCore.MemberId,
                Amount = serviceChargeCore.Amount
            };
        }


        public static ServiceChargeCore ToCoreModel(ServiceChargeViewModel serviceChargeViewModel)
        {
            return new ServiceChargeCore
            {
                EmpId = serviceChargeViewModel.EmpId,
                MemberId = serviceChargeViewModel.MemberId,
                Amount = serviceChargeViewModel.Amount
            };
        }
    }
}
