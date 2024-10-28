using PaymentSystem.Application.Payment;
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

        public void AddCompensationAlterEvent(string empId, int amount, DateOnly startDate, Models.BasicDataMaintenece.Employee.PayWayEnum hourly)
        {
            CompensationAlterEventDbModel compensationAlterEventDbModel = new CompensationAlterEventDbModel
            {
                EmpId = empId,
                Amount = amount,
                StartDate = startDate
            };

            this._appDbContext.CompensationAlterEvents.Add(compensationAlterEventDbModel);
        }

        public void AddPaymentEvent(string empId, DateOnly dateOnly, Models.BasicDataMaintenece.Employee.PayWayEnum hourly)
        {
            this._appDbContext.PaymentEvents.Add(new PaymentEventDbModel
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = empId,
                PayDate = dateOnly,
            });
        }

        public void AddSalary(EmpSalary amountCore)
        {
            CompensationAlterEventDbModel empSalaryDbModel = new CompensationAlterEventDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Amount
            };

            this._appDbContext.CompensationAlterEvents.Add(empSalaryDbModel);
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

        public void AddTimeCard(TimeCard timeCard)
        {
            throw new NotImplementedException();
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

        public IEnumerable<TimeCard> GetTimeCards(string id)
        {
            throw new NotImplementedException();
        }

        public Models.Payment.Employee Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.First(e => e.EmpId == empId);
            return EmpFactory.Build(empId, empDbModel.PayWay, this);
        }
    }
}
