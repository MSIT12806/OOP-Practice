using Humanizer;
using PaymentSystem.Models;
using PaymentSystem.Models.Payment;

namespace PaymentSystem.Application.Payday
{
    public class PaydayService
    {
        private IPaydayRepository _paydayRepopsitory;
        private IServiceChargeSetter _serviceChargeSetter;
        private ISalesReceiptSetter _salesReceiptSetter;
        private IPaymentRepository _empRepository;
        private IEmpRepository empRepository1;

        public PaydayService(IServiceChargeSetter serviceChargeGetter, IPaydayRepository paydayRepopsitory, ISalesReceiptSetter salesReceiptSetter, IPaymentRepository paymentRepository, IEmpRepository empRepository)
        {
            this._serviceChargeSetter = serviceChargeGetter;
            this._paydayRepopsitory = paydayRepopsitory;
            this._salesReceiptSetter = salesReceiptSetter;
            this._empRepository = paymentRepository;
            this.empRepository1 = empRepository;
        }

        public IEnumerable<Payroll> Pay(DateOnly date)
        {
            var recordes = _paydayRepopsitory.GetPayRecordsBy(date);
            if (recordes.Any())
            {
                throw new InvalidOperationException("Already paid");
            }

            var emps = this.empRepository1.GetEmpIds();

            var paydays = emps.Select(i=> this._empRepository.Rebuild(i).Settle()).ToList();

            return paydays;
        }

        private Payroll ToPaydayCore(EmpSalaryCore salaryCore)
        {
            return new Payroll
            {
                EmpId = salaryCore.EmpId,
                Salary = salaryCore.Amount,
            };
        }

        public void SetSalary(EmpSalaryCore salaryCore)
        {
            var core = _empRepository.Rebuild(salaryCore.EmpId) as MounthlyEmployee;
            core.SetSalary(salaryCore.Amount);
        }

        public EmpSalaryCore GetEmpSalary(string empId)
        {
            var core = _empRepository.Rebuild(empId) as MounthlyEmployee;
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
