using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public interface IServiceChargeRepository
    {
        void SetServiceCharge(ServiceChargeCore serviceCharge);
        IEnumerable<ServiceChargeCore> GetServiceCharges();
    }
}
