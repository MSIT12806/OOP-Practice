using LH.Tool.Decoupling;
using Payment.Models.Payment;
using System;

namespace Payment.Application
{
    public class PaymentService
    {
        private IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            this._paymentRepository = paymentRepository;
        }

        public Employee Rebuild(string empId)
        {
            return this._paymentRepository.Rebuild(empId);
        }

        public Payroll Pay(DateTime date)
        {
            throw new System.NotImplementedException();
        }

        public void Build(string empId, int amount, DateTime startDate)
        {
            var emp = this.Rebuild(empId);
            emp.AddCompensationAlterEvent(amount, startDate);
            emp.AddPaymentEvent(DateProvider.Now);
        }
    }
}
