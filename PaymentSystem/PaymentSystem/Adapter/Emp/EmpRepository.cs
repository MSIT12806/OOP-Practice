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

        public void AddEmp(EmpDbModel empDbModel)
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

        public IEnumerable<EmpCore> GetList()
        {
            List<EmpCore> empList = new List<EmpCore>();

            foreach (EmpDbModel empDbModel in this._appDbContext.Emps)
            {
                empList.Add(this.ToCoreModel(empDbModel));
            }

            return empList;
        }

        public EmpCore GetSingle(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FirstOrDefault(e => e.EmpId == empId);

            return this.ToCoreModel(empDbModel);
        }

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

        public void DeleteSalesReceiptBy(string salesReceiptId)
        {
            var salesReceipt = _appDbContext.SalesReceipts.FirstOrDefault(s => s.Id == salesReceiptId);
            if (salesReceipt != null)
            {
                _appDbContext.SalesReceipts.Remove(salesReceipt);
                _appDbContext.SaveChanges();
            }
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
            return new EmpCore
            {
                Id = empDbModel.EmpId,
                Name = empDbModel.Name,
                Address = empDbModel.Address
            };
        }

        // DDD 重構

        public void InjectData(EmpCore empCore)
        {
            var empDbModel = this._appDbContext.Emps.FirstOrDefault(e => e.EmpId == empCore.Id);
            if (empDbModel != null)
            {
                empCore.InjectData(empDbModel.Name, empDbModel.Address);
                return;
            }

            throw new InvalidDataException("Emp not found");
        }
    }
}
