using NuGet.Protocol.Core.Types;
using PaymentSystem.Models;
using PaymentSystem.Models.BasicDataMaintenece;

namespace PaymentSystem.Application
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
            _empRepository.Build(empId, name, address);
        }

        public Employee Rebuild(string empId)
        {
            return this._empRepository.Rebuild(empId);
        }

        public void ChgEmpName(string empId, string name)
        {
           _empRepository.ChgEmpName(empId,name);
        }

        public void ChgEmpAddress(string empId, string address)
        {
            _empRepository.ChgEmpAddress(empId,address);
        }
    }
}
