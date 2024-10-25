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

        public void SetServiceCharge(IEnumerable<Payment> paydays)
        {
            var serviceCharges = _service.GetList();

            foreach (var payday in paydays)
            {
                var serviceChargeByEmpId = serviceCharges.Where(x => x.EmpId == payday.EmpId);

                if (serviceChargeByEmpId != null && serviceChargeByEmpId.Count() != 0)
                {
                    payday.ServiceCharge = serviceChargeByEmpId.Sum(i => i.Amount);
                }
            }
        }
    }

}
