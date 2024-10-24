using PaymentSystem.Application.Emp;
using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public class EmpRepository : IEmpRepository
    {
        private AppDbContext _appDbContext;
        public EmpRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        private void AddEmp(EmpDbModel empDbModel)
        {
            this._appDbContext.Emps.Add(empDbModel);
            this._appDbContext.SaveChanges();
        }
        public void Add(EmpCore emp)
        {
            EmpDbModel empDbModel = this.ToDbModel(emp);
            this.AddEmp(empDbModel);
        }
        public void Update(EmpCore empCore)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FirstOrDefault(e => e.EmpId == empCore.Id);

            empDbModel.Name = empCore.Name;
            empDbModel.Address = empCore.Address;

            this._appDbContext.SaveChanges();
        }
        public EmpCore Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FirstOrDefault(e => e.EmpId == empId);

            return this.ToCoreModel(empDbModel);
        }
        public IEnumerable<EmpCore> GetList()
        {
            List<EmpCore> empList = new List<EmpCore>();

            foreach (EmpDbModel empDbModel in this._appDbContext.Emps)
            {
                empList.Add(this.ToCoreModel(empDbModel));
            }

            return empList;
        }
        private EmpDbModel ToDbModel(EmpCore emp)
        {
            return new EmpDbModel
            {
                EmpId = emp.Id,
                Name = emp.Name,
                Address = emp.Address
            };
        }
        private EmpCore ToCoreModel(EmpDbModel empDbModel)
        {
            var emp = new EmpCore(empDbModel.EmpId, this);
            emp.InitialData(empDbModel.Name, empDbModel.Address);
            return emp;
        }

        // SalesReceipt

        public string AddSalesReceipt(SalesReceiptCore salesReceipt)
        {
            var Id = Guid.NewGuid().ToString();

            var dbData = _appDbContext.SalesReceipts.Add(new SalesReceiptDbModel
            {
                Id = Id,
                EmpId = salesReceipt.EmpId,
                SalesDate = salesReceipt.SalesDate,
                Commission = salesReceipt.Commission
            });
            _appDbContext.SaveChanges();

            return Id;
        }
        public IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId)
        {
            List<SalesReceiptCore> salesReceiptList = new List<SalesReceiptCore>();

            foreach (SalesReceiptDbModel salesReceiptDbModel in this._appDbContext.SalesReceipts.Where(s => s.EmpId == empId))
            {
                salesReceiptList.Add(new SalesReceiptCore
                {
                    Id = salesReceiptDbModel.Id,
                    EmpId = salesReceiptDbModel.EmpId,
                    SalesDate = salesReceiptDbModel.SalesDate,
                    Commission = salesReceiptDbModel.Commission
                });
            }

            return salesReceiptList;
        }
        public IEnumerable<ServiceChargeCore> GetServiceCharges(string empId)
        {
            List<ServiceChargeCore> serviceChargeList = new List<ServiceChargeCore>();

            foreach (ServiceChargeDbModel serviceChargeDbModel in this._appDbContext.ServiceCharges.Where(s => s.EmpId == empId))
            {
                serviceChargeList.Add(new ServiceChargeCore
                {
                    Id = serviceChargeDbModel.ServiceChargeId,
                    EmpId = serviceChargeDbModel.EmpId,
                    Amount = serviceChargeDbModel.ServiceCharge,
                    ApplyDate = serviceChargeDbModel.ApplyDate
                });
            }

            return serviceChargeList;
        }
        public void DeleteServiceChargeBy(string serviceChargeId)
        {
            var serviceCharge = _appDbContext.ServiceCharges.FirstOrDefault(s => s.ServiceChargeId == serviceChargeId);
            if (serviceCharge != null)
            {
                _appDbContext.ServiceCharges.Remove(serviceCharge);
                _appDbContext.SaveChanges();
            }
        }
        public string AddServiceCharge(ServiceChargeCore serviceCharge)
        {
            var Id = Guid.NewGuid().ToString();

            var dbData = _appDbContext.ServiceCharges.Add(new ServiceChargeDbModel
            {
                ServiceChargeId = Id,
                EmpId = serviceCharge.EmpId,
                ServiceCharge = serviceCharge.Amount,
                ApplyDate = serviceCharge.ApplyDate
            });

            _appDbContext.SaveChanges();

            return Id;
        }
        public void DeleteSalesReceiptBy(string salesReceiptId)
        {
            var salesReceipt = _appDbContext.SalesReceipts.FirstOrDefault(s => s.Id == salesReceiptId);
            if (salesReceipt != null)
            {
                _appDbContext.SalesReceipts.Remove(salesReceipt);
                _appDbContext.SaveChanges();
            }
        }

        // Salary

        public void AddSalary(EmpSalaryCore amountCore)
        {
            var dbModel = this._appDbContext.Salaries.SingleOrDefault(x => x.EmpId == amountCore.EmpId);
            if (dbModel == null)
            {
                dbModel = this.ToDbModel(amountCore);
                this._appDbContext.Salaries.Add(dbModel);
                this._appDbContext.SaveChanges();
                return;
            }

            if (this._appDbContext.Salaries.Any(x => x.EmpId == amountCore.EmpId))
            {
                this._appDbContext.Update(dbModel, this.ToDbModel(amountCore));
                this._appDbContext.SaveChanges();
                return;
            }
        }
        public EmpSalaryCore GetSalary(string empId)
        {
            var dbModel = this._appDbContext.Salaries
                .Where(x => x.EmpId == empId)
                .OrderByDescending(i => i.CreateDatetime)
                .FirstOrDefault();

            if (dbModel == null)
            {
                throw new InvalidDataException("Salary not found");
            }

            return ToCoreModel(dbModel);
        }
        public IEnumerable<EmpSalaryCore> GetEmpSalaries()
        {
            return this._appDbContext.Salaries.ToList().Select(this.ToCoreModel);
        }
        private EmpSalaryCore ToCoreModel(SalaryDbModel source)
        {
            return new EmpSalaryCore(source.EmpId, source.Amount, (EmpSalaryCore.PayWayEnum)source.PayWay);
        }
        private SalaryDbModel ToDbModel(EmpSalaryCore amountCore)
        {
            return new SalaryDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Amount,
            };
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
            this._appDbContext.SaveChanges();
            this.Disposed = true;
        }
        public bool Disposed { get; private set; }  
    }
}
