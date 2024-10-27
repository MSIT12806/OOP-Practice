﻿using PaymentSystem.Models;
using PaymentSystem.Models.Payment;

namespace PaymentSystem.Application
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

        public Payroll Pay(DateOnly date)
        {
            throw new System.NotImplementedException();
        }
    }
}
