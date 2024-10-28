

using System;
using System.Collections.Generic;

namespace Payment.Models.Payment
{
    public interface IPaymentRepository
    {
        Employee Rebuild(string empId);
        string AddSalesReceipt(SalesReceipt salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<SalesReceipt> GetSalesReceipts(string empId);

        void AddSalary(EmpSalary amountCore);
        IEnumerable<EmpSalary> GetSalaries();
        EmpSalary GetSalary(string empId);

        IEnumerable<ServiceCharge> GetServiceCharges(string id);
        void DeleteServiceChargeBy(string setviceChargeId);
        string AddServiceCharge(ServiceCharge serviceCharge);

        IEnumerable<TimeCard> GetTimeCards(string id);
        void AddTimeCard(TimeCard timeCard);
        void AddPaymentEvent(string empId, DateTime dateOnly, string employeeType);
        void AddCompensationAlterEvent(string empId, int amount, DateTime startDate, string employeeType);
    }
}
