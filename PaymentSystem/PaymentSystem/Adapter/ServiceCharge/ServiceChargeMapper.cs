using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public class ServiceChargeMapper
    {
        public ServiceChargeViewModel ToViewModel(ServiceChargeCore serviceChargeCore)
        {
            return new ServiceChargeViewModel
            {
                EmpId = serviceChargeCore.EmpId,
                MemberId = serviceChargeCore.MemberId,
                Amount = serviceChargeCore.Amount
            };
        }


        public ServiceChargeCore ToCoreModel(ServiceChargeViewModel serviceChargeViewModel)
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
