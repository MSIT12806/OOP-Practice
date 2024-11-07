using Payment.Models.BasicDataMaintenece;

namespace Payment.Application
{
    public class EmpDataService
    {
        private IEmpDataRepository _empRepository;

        public EmpDataService(IEmpDataRepository empRepository)
        {
            this._empRepository = empRepository;
        }

        public void Build(string empId, string name, string address)
        {
            this._empRepository.Build(empId, name, address);
        }

        public Employee Rebuild(string empId)
        {
            return this._empRepository.Rebuild(empId);
        }

        public void ChgEmpName(string empId, string name)
        {
            this._empRepository.ChgEmpName(empId, name);
        }

        public void ChgEmpAddress(string empId, string address)
        {
            this._empRepository.ChgEmpAddress(empId, address);
        }
    }
}
