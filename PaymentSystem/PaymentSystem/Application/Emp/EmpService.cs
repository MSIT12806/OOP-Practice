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

        public EmpCore InstantiationEmp(string empId, string name, string address)
        {
            var emp = new EmpCore(empId, this._empRepository);
            emp.InitialData(name, address);

            return emp;
        }
        public void AddEmp(EmpCore emp)
        {
            this._empRepository.Add(emp);
        }

        public void ChgEmp(string empId, string name, string address)
        {
            var emp = new EmpCore(empId, this._empRepository);
            emp.Update(name, address);
        }


        public IEnumerable<EmpCore> GetList()
        {
            return this._empRepository.GetList();
        }

        public EmpCore GetSingle(string empId)
        {
            return this._empRepository.GetSingle(empId);
        }

        public string AddSalesReceipt(string id, DateOnly dateOnly, int commission)
        {
            var salesReceipt = new SalesReceiptCore
            {
                EmpId = id,
                SalesDate = dateOnly,
                Commission = commission
            };

            return this._empRepository.AddSalesReceipt(salesReceipt);
        }

        public IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId)
        {
            return this._empRepository.GetSalesReceipts(empId);
        }

        public void DeleteSalesReceiptBy(string salesReceiptId)
        {
            this._empRepository.DeleteSalesReceiptBy(salesReceiptId);
        }
    }
}
