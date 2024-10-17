namespace PaymentSystem.Application.Payday
{
    public class PaydayService
    {
        private IEmpGetter _empGetter;
        private IServiceChargeGetter _serviceChargeGetter;

        public PaydayService(IEmpGetter empGetter, IServiceChargeGetter serviceChargeGetter)
        {
            _empGetter = empGetter;
            _serviceChargeGetter = serviceChargeGetter;
        }

        public IEnumerable<PaydayResult> Pay()
        {
            var emps = _empGetter.GetEmps();
            var serviceCharges = _serviceChargeGetter.GetServiceCharges();

            var results = new List<PaydayResult>();

            foreach (var emp in emps)
            {
                var serviceCharge = serviceCharges.FirstOrDefault(x => x.EmpId == emp.EmpId);

                results.Add(new PaydayResult
                {
                    EmpId = emp.EmpId,
                    Salary = emp.Salary,
                    ServiceCharge = serviceCharge?.Amount
                });
            }

            return results;
        }
    }

}
