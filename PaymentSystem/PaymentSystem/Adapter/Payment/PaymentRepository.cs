using Payment.Application.Payment;
using Payment.Models.Payment;
using PaymentSystem.Infrastructure.ORM;

namespace PaymentSystem.Adapter.Payment
{
    public class PaymentRepository : IPaymentRepository, IAsyncDisposable
    {
        private AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddCompensationAlterEvent(string empId, int amount, DateTime startDate, string compensationType)
        {
            CompensationAlterEventDbModel compensationAlterEventDbModel = new CompensationAlterEventDbModel
            {
                EmpId = empId,
                Amount = amount,
                StartDate = DateOnly.FromDateTime(startDate),
                CompensationType = compensationType
            };

            this._appDbContext.CompensationAlterEvents.Add(compensationAlterEventDbModel);
        }

        public void AddPaymentEvent(string empId, DateTime dateOnly, string compensationType)
        {
            this._appDbContext.PaymentEvents.Add(new PaymentEventDbModel
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = empId,
                PayDate = DateOnly.FromDateTime(dateOnly),
                CompensationType = compensationType
            });
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
            throw new NotImplementedException();
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

        public Employee Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FindById(empId);
            Type type = null;
            switch (empDbModel.PayWay)
            {
                case nameof(HourlyEmployee):
                    type = typeof(HourlyEmployee);
                    break;

                case nameof(MounthlyEmployee):
                    type = typeof(MounthlyEmployee);
                    break;

                case nameof(SalesEmployee):
                    type = typeof(SalesEmployee);
                    break;

                case nameof(UnionEmployee):
                    type = typeof(UnionEmployee);
                    break;

                default:
                    throw new ArgumentException("Invalid employee type", empId);
            }
            return EmpFactory.Build(empId, type, this);
        }
    }
}
