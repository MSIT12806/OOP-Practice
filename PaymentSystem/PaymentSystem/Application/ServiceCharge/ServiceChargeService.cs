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
        public string AddServiceCharge(string empId, int amount, DateOnly date)
        {
            var serviceCharge = this.GenerateNewOne(empId, amount, date);
            if (!this._empExistChecker.Check(serviceCharge.EmpId))
            {
                throw new InvalidOperationException("Emp is not exist");
            }

            return this._serviceChargeRepository.AddServiceCharge(serviceCharge);
        }

        public void DeleteServiceCharge(string serviceChargeId)
        {
            this._serviceChargeRepository.DeleteServiceCharge(serviceChargeId);
        }

        public IEnumerable<ServiceChargeCore> GetList()
        {
            return this._serviceChargeRepository.GetServiceCharges();
        }

        public IEnumerable<ServiceChargeCore> GetListBy(string empId)
        {
            return this._serviceChargeRepository.GetServiceCharges().Where(x => x.EmpId == empId);
        }

        public ServiceChargeCore GetSingle(string id)
        {
            return this._serviceChargeRepository.GetServiceCharges().FirstOrDefault(x => x.Id == id);
        }

        public ServiceChargeCore GenerateNewOne(string empId, int amount, DateOnly date)
        {

            return new ServiceChargeCore
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = empId,
                Amount = amount,
                ApplyDate = date,
            };
        }
    }
}
