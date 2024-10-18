using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;
namespace PaymentSystem.Adapter.Payday
{
    public class ServiceChargeGetter : IServiceChargeGetter
    {
        private ServiceChargeService _service;

        public ServiceChargeGetter(ServiceChargeService serviceChargeService)
        {
            this._service = serviceChargeService;
        }
        public IEnumerable<ServiceChargeForPay> GetServiceCharges()
        {
            var result = this._service.GetList().Select(i => new ServiceChargeForPay
            {
                Amount = i.Amount,
                EmpId = i.EmpId,
            });

            return result;
        }
    }
}
