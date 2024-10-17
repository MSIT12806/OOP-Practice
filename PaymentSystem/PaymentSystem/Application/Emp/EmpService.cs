using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public class EmpService
    {
        private IEmpRepository _empRepository;


        public EmpService(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }
        public void AddEmp(EmpCore emp)
        {
            _empRepository.AddEmp(emp);
        }

        public IEnumerable<EmpCore> GetList()
        {
            return _empRepository.GetList();
        }
    }
}
