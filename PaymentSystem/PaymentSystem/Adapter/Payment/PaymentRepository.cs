using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models.Payment;

namespace PaymentSystem.Adapter.Payment
{
    public class PaymentRepository : IPaymentRepository, IAsyncDisposable
    {
        private AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddSalary(EmpSalary amountCore)
        {
            SalaryDbModel empSalaryDbModel = new SalaryDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Amount
            };

            this._appDbContext.Salaries.Add(empSalaryDbModel);
        }

        public string AddSalesReceipt(SalesReceipt salesReceipt)
        {
            SalesReceiptDbModel salesReceiptDbModel = new SalesReceiptDbModel
            {
                EmpId = salesReceipt.EmpId,
            };

            this._appDbContext.SalesReceipts.Add(salesReceiptDbModel);
            return salesReceiptDbModel.Id;
        }

        public string AddServiceCharge(ServiceCharge serviceCharge)
        {
            ServiceChargeDbModel serviceChargeDbModel = new ServiceChargeDbModel
            {
                EmpId = serviceCharge.EmpId,
            };

            this._appDbContext.ServiceCharges.Add(serviceChargeDbModel);
            return serviceChargeDbModel.ServiceChargeId;
        }

        public void DeleteSalesReceiptBy(string salesReceiptId)
        {

        }

        public void DeleteServiceChargeBy(string setviceChargeId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("Dispose");
            this._appDbContext.SaveChanges();
        }

        public IEnumerable<EmpSalary> GetSalaries()
        {
            throw new NotImplementedException();
        }

        public EmpSalary GetSalary(string empId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReceipt> GetSalesReceipts(string empId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceCharge> GetServiceCharges(string id)
        {
            throw new NotImplementedException();
        }

        public Models.Payment.Employee Rebuild(string empId)
        {
            throw new NotImplementedException();
        }
    }
}
