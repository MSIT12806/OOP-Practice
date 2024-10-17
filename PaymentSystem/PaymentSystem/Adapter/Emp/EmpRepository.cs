using PaymentSystem.Application;
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
            _appDbContext = appDbContext;
        }

        public void AddEmp(EmpDbModel empDbModel)
        {
            _appDbContext.Emps.Add(empDbModel);
            _appDbContext.SaveChanges();
        }

        public void Add(EmpCore emp)
        {
            EmpDbModel empDbModel = ToDbModel(emp);

            AddEmp(empDbModel);
        }

        public IEnumerable<EmpCore> GetList()
        {
            List<EmpCore> empList = new List<EmpCore>();

            foreach (EmpDbModel empDbModel in _appDbContext.Emps)
            {
                empList.Add(ToCoreModel(empDbModel));
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
            return new EmpCore
            {
                Id = empDbModel.EmpId,
                Name = empDbModel.Name,
                Address = empDbModel.Address
            };
        }
    }
}
