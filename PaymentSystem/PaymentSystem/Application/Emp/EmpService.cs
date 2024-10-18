using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public class EmpService
    {
        private IEmpRepository _empRepository;


        public EmpService(IEmpRepository empRepository)
        {
            this._empRepository = empRepository;
        }
        public void AddEmp(EmpCore emp)
        {
            this._empRepository.Add(emp);
        }

        public IEnumerable<EmpCore> GetList()
        {
            return this._empRepository.GetList();
        }

        public EmpCore GetSingle(string empId)
        {
            return this._empRepository.GetSingle(empId);
        }
    }
}
