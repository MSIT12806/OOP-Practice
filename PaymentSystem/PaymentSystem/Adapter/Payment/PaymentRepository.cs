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
                EmployeeType = compensationType
            };

            this._appDbContext.CompensationAlterEvents.Add(compensationAlterEventDbModel);
        }

        public void AddPaymentPlan(string empId, DateTime dateOnly, string compensationType)
        {
            this._appDbContext.PaymentPlans.Add(new PaymentPlanDbModel
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = empId,
                PayDate = dateOnly,
                CompensationType = compensationType
            });
        }

        public void AddPayroll(Payroll payroll)
        {
            PayrollDbModel payrollDbModel = new PayrollDbModel
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = payroll.EmpId,
                PayDate = payroll.PayDate,
                Amount = payroll.TotoalPay
            };

            foreach (var payrollDetail in payroll.PayrollDetails)
            {
                this._appDbContext.PayrollDetails.Add(new PayrollDetailDbModel
                {
                    PayrollId = payrollDbModel.Id,
                    Description = payrollDetail.Description,
                    Amount = payrollDetail.Amount
                });
            }

            this._appDbContext.Payrolls.Add(payrollDbModel);
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
            TimeCardDbModel timeCardDbModel = new TimeCardDbModel
            {
                Id = Guid.NewGuid().ToString(),
                EmpId = timeCard.EmpId,
                WorkDate = timeCard.WorkDate,
                Hours = timeCard.Hours
            };

            this._appDbContext.TimeCards.Add(timeCardDbModel);
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

        public DateTime? GetPaymentEventByRecently(string empId, DateTime payDate)
        {
            return this._appDbContext.Payrolls
                .Where(i => i.EmpId == empId)
                .Where(i => i.PayDate < payDate)
                .OrderByDescending(i => i.PayDate)
                .FirstOrDefault()?.PayDate;
        }

        public IEnumerable<EmpSalary> GetSalaries()
        {
            throw new NotImplementedException();
        }

        public EmpSalary GetSalary(string empId)
        {
            var newestSalarySetting = this._appDbContext.CompensationAlterEvents.Where(i => i.EmpId == empId).OrderByDescending(i => i.CreateDateTime).First();

            return new EmpSalary(empId, newestSalarySetting.Amount, newestSalarySetting.EmployeeType);
        }

        public IEnumerable<SalesReceipt> GetSalesReceipts(string empId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceCharge> GetServiceCharges(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeCard> GetTimeCards(string empId)
        {
            var timeCardDbModels = this._appDbContext.TimeCards.Where(i => i.EmpId == empId);
            return timeCardDbModels.Select(i => new TimeCard(i.EmpId, i.WorkDate, i.Hours));
        }

        public Employee Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FindById(empId);
            EmpSalary empSalary = this.GetSalary(empId);
            return EmpFactory.Build(empId, empSalary.EmployeeType, this);
        }
    }
}
