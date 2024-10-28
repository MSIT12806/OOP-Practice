using Payment.Models.BasicDataMaintenece;
using PaymentSystem.Infrastructure.ORM;
using static Payment.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Adapter.BasicDataMaintenence
{
    public class EmpDataRepository : IEmpDataRepository, IAsyncDisposable
    {
        private AppDbContext _appDbContext;


        public EmpDataRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void Build(string empId, string name, string address, string payWay)
        {
            EmpDbModel empDbModel = new EmpDbModel
            {
                EmpId = empId,
                Name = name,
                Address = address,
                PayWay = payWay.ToString()
            };

            this._appDbContext.Emps.Add(empDbModel);
        }
        public Employee Rebuild(string empId)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FindById( empId);

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
            var emp = this._appDbContext.Emps.FindById(empId);
            emp.Name = name;
            this._appDbContext.Emps.Update(emp);
        }

        public void ChgEmpAddress(string empId, string address)
        {
            var emp = this._appDbContext.Emps.FindById(empId);
            emp.Address = address;
            this._appDbContext.Emps.Update(emp);
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("Dispose");
            this._appDbContext.SaveChanges();
        }

    }
}
