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

        public void Update(EmpCore empCore)
        {
            EmpDbModel empDbModel = this._appDbContext.Emps.FirstOrDefault(e => e.EmpId == empCore.Id);

            empDbModel.Name = empCore.Name;
            empDbModel.Address = empCore.Address;

            this._appDbContext.SaveChanges();
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

    }
}
