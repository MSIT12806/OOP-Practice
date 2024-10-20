using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public interface IServiceChargeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Id</returns>
        string AddServiceCharge(ServiceChargeCore serviceCharge);
        void DeleteServiceCharge(string serviceChargeId);
        IEnumerable<ServiceChargeCore> GetServiceCharges();
    }
}
