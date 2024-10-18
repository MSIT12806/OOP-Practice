using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public class ServiceChargeService
    {
        private IServiceChargeRepository _serviceChargeRepository;
        private IEmpExistChecker _empExistChecker;

        public ServiceChargeService(IServiceChargeRepository serviceChargeRepository, IEmpExistChecker empExistChecker)
        {
            this._serviceChargeRepository = serviceChargeRepository;
            this._empExistChecker = empExistChecker;
        }
        public void SetServiceCharge(ServiceChargeCore serviceCharge)
        {
            if (!this._empExistChecker.Check(serviceCharge.EmpId))
            {
                throw new InvalidOperationException("Emp is not exist");
            }

            this._serviceChargeRepository.SetServiceCharge(serviceCharge);
        }

        public IEnumerable<ServiceChargeCore> GetList()
        {
            return this._serviceChargeRepository.GetServiceCharges();
        }
    }
}
