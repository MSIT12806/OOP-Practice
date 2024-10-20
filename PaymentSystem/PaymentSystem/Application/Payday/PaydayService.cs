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
            var emps = this._paydayRepopsitory.GetEmpSalaries().ToList().Select(ToPaydayCore).ToList();

            this._serviceChargeSetter.SetServiceCharge(emps);
            this._salesReceiptSetter.SetSalesReceipt(emps);

            return emps;
        }

        private PaydayCore ToPaydayCore(EmpSalaryCore salaryCore)
        {
            return new PaydayCore
            {
                EmpId = salaryCore.EmpId,
                Salary = salaryCore.Salary,
            };
        }

        public void SetSalary(EmpSalaryCore salaryCore)
        {
            this._paydayRepopsitory.Save(salaryCore);
        }

        public EmpSalaryCore GetEmpSalary(string empId)
        {
            return this._paydayRepopsitory.GetEmpSalaries().FirstOrDefault(x => x.EmpId == empId);
        }

        public IEnumerable<TimeCardCore> GetTimeCards(string empId)
        {
            IEnumerable<TimeCardCore> timeCardCoreList = _paydayRepopsitory.GetTimeCards(empId);
            return timeCardCoreList;
        }

        public TimeCardCore GetTimeCard(string timeCardId)
        {
            return this._paydayRepopsitory.GetTimeCard(timeCardId);
        }
    }
}
