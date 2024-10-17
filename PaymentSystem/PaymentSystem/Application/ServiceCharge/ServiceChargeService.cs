using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public class ServiceChargeService
    {
        private IServiceChargeRepository _serviceChargeRepository;
        private IEmpExistChecker _empExistChecker;

        public ServiceChargeService(IServiceChargeRepository serviceChargeRepository, IEmpExistChecker empExistChecker)
        {
            _serviceChargeRepository = serviceChargeRepository;
            _empExistChecker = empExistChecker;
        }
        public void SetServiceCharge(ServiceChargeCore serviceCharge)
        {
            if (!_empExistChecker.Check(serviceCharge.EmpId))
            {
                throw new InvalidOperationException("Emp is not exist");
            }

            _serviceChargeRepository.SetServiceCharge(serviceCharge);
        }
    }
}
