using Humanizer;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public class PaydayService
    {
        private IPaydayRepository _paydayRepopsitory;
        private IServiceChargeSetter _serviceChargeSetter;
        private ISalesReceiptSetter _salesReceiptSetter;

        public PaydayService(IServiceChargeSetter serviceChargeGetter, IPaydayRepository paydayRepopsitory, ISalesReceiptSetter salesReceiptSetter)
        {
            this._serviceChargeSetter = serviceChargeGetter;
            this._paydayRepopsitory = paydayRepopsitory;
            this._salesReceiptSetter = salesReceiptSetter;
        }

        public IEnumerable<PaydayCore> Pay()
        {
            var emps = this._paydayRepopsitory.GetEmpSalaries().ToList().Select(ToPaydayCore);

            var results = new List<PaydayCore>();

            this._serviceChargeSetter.SetServiceCharge(emps);
            this._salesReceiptSetter.SetSalesReceipt(emps);

            return results;
        }

        private PaydayCore ToPaydayCore(EmpSalaryCore salaryCore)
        {
            return new PaydayCore
            {
                EmpId = salaryCore.EmpId,
                Salary = salaryCore.Salary,
            };
        }

        public void SaveSalary(EmpSalaryCore salaryCore)
        {
            this._paydayRepopsitory.Save(salaryCore);
        }

        public EmpSalaryCore GetSingle(string empId)
        {
            return this._paydayRepopsitory.GetEmpSalaries().FirstOrDefault(x => x.EmpId == empId);
        }
    }
}
