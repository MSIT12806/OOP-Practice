using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public class ServiceChargeSetter : IServiceChargeSetter
    {
        private ServiceChargeService _service;

        public ServiceChargeSetter(ServiceChargeService serviceChargeService)
        {
            this._service = serviceChargeService;
        }

        public void SetServiceCharge(IEnumerable<PaydayCore> paydays)
        {
            var serviceCharges = _service.GetList();

            foreach (var payday in paydays)
            {
                var serviceCharge = serviceCharges.FirstOrDefault(x => x.EmpId == payday.EmpId);

                if (serviceCharge != null)
                {
                    payday.ServiceCharge = serviceCharge.Amount;
                }
            }
        }
    }
}
