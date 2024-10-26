using Microsoft.EntityFrameworkCore;
using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;
using PaymentSystem.Models.BasicDataMaintenece;

namespace PaymentSystem.Adapter
{
    public class EmpRepository : IEmpRepository, IAsyncDisposable
    {
        private AppDbContext _appDbContext;


        public EmpRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void Build(string empId, string name, string address)
        {
            EmpDbModel empDbModel = new EmpDbModel
            {
                EmpId = empId,
                Name = name,
                Address = address
            };

            this._appDbContext.Emps.Add(empDbModel);
        }
        public Employee Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.First(e => e.EmpId == empId);

            return new Employee
            {
                Id = empDbModel.EmpId,
                Name = empDbModel.Name,
                Address = empDbModel.Address,
                PayWay = empDbModel.PayWay
            };
        }
        public IEnumerable<string> GetEmpIds()
        {
            return this._appDbContext.Emps.Select(e => e.EmpId).ToList();
        }
        public void ChgEmpName(string empId, string name)
        {
            var emp = this._appDbContext.Emps.First(e => e.EmpId == empId);
            emp.Name = name;
        }

        public void ChgEmpAddress(string empId, string address)
        {
            var emp = this._appDbContext.Emps.First(e => e.EmpId == empId);
            emp.Address = address;
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("Dispose");
            this._appDbContext.SaveChanges();
        }

        

    }

    public class PaymentRepository : IPaymentRepository, IAsyncDisposable
    {
        private AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddSalary(EmpSalaryCore amountCore)
        {
            SalaryDbModel empSalaryDbModel = new SalaryDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Amount
            };

            this._appDbContext.Salaries.Add(empSalaryDbModel);
        }

        public string AddSalesReceipt(SalesReceiptCore salesReceipt)
        {
            SalesReceiptDbModel salesReceiptDbModel = new SalesReceiptDbModel
            {
                EmpId = salesReceipt.EmpId,
            };

            this._appDbContext.SalesReceipts.Add(salesReceiptDbModel);
            return salesReceiptDbModel.Id;
        }

        public string AddServiceCharge(ServiceChargeCore serviceCharge)
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

        public IEnumerable<EmpSalaryCore> GetSalaries()
        {
            throw new NotImplementedException();
        }

        public EmpSalaryCore GetSalary(string empId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceChargeCore> GetServiceCharges(string id)
        {
            throw new NotImplementedException();
        }

        public Models.Payment.Employee Rebuild(string empId)
        {
            throw new NotImplementedException();
        }
    }
}
