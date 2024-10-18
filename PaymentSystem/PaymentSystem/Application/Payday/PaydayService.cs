using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public class PaydayService
    {
        private IServiceChargeGetter _serviceChargeGetter;
        private IPaydayRepository _paydayRepopsitory;

        public PaydayService(IServiceChargeGetter serviceChargeGetter, IPaydayRepository paydayRepopsitory)
        {
            this._serviceChargeGetter = serviceChargeGetter;
            this._paydayRepopsitory = paydayRepopsitory;
        }

        public IEnumerable<PaydayResult> Pay()
        {
            var emps = this._paydayRepopsitory.GetAmounts();
            var serviceCharges = this._serviceChargeGetter.GetServiceCharges();

            var results = new List<PaydayResult>();

            foreach (var emp in emps)
            {
                var serviceCharge = serviceCharges.FirstOrDefault(x => x.EmpId == emp.EmpId);

                results.Add(new PaydayResult
                {
                    EmpId = emp.EmpId,
                    Salary = emp.Amount,
                    ServiceCharge = serviceCharge?.Amount
                });
            }

            return results;
        }

        public void SaveAmount(AmountCore amountCore)
        {
            this._paydayRepopsitory.Save(amountCore);
        }

        public AmountCore GetSingle(string empId)
        {
            return this._paydayRepopsitory.GetAmounts().FirstOrDefault(x => x.EmpId == empId);
        }
    }
}
