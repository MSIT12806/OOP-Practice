using Humanizer;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public class PaydayService
    {
        private IPaydayRepository _paydayRepopsitory;
        private IServiceChargeSetter _serviceChargeSetter;
        private ISalesReceiptSetter _salesReceiptSetter;
        private IEmpRepository _empRepository;

        public PaydayService(IServiceChargeSetter serviceChargeGetter, IPaydayRepository paydayRepopsitory, ISalesReceiptSetter salesReceiptSetter, IEmpRepository empRepository)
        {
            this._serviceChargeSetter = serviceChargeGetter;
            this._paydayRepopsitory = paydayRepopsitory;
            this._salesReceiptSetter = salesReceiptSetter;
            this._empRepository = empRepository;
        }

        public IEnumerable<Payment> Pay(DateOnly date)
        {
            var recordes = _paydayRepopsitory.GetPayRecordsBy(date);
            if (recordes.Any())
            {
                throw new InvalidOperationException("Already paid");
            }

            var emps = this._empRepository.GetList();

            var paydays = emps.Select(i=>i.Pay()).ToList();

            return paydays;
        }

        private Payment ToPaydayCore(EmpSalaryCore salaryCore)
        {
            return new Payment
            {
                EmpId = salaryCore.EmpId,
                Salary = salaryCore.Amount,
            };
        }

        public void SetSalary(EmpSalaryCore salaryCore)
        {
            var core = _empRepository.Rebuild(salaryCore.EmpId);
            core.SetSalary(salaryCore.Amount, salaryCore.PayWay);
        }

        public EmpSalaryCore GetEmpSalary(string empId)
        {
            var core = _empRepository.Rebuild(empId);
            return core.GetSalary();
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
