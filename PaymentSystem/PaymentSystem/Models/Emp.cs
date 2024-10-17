using Microsoft.AspNetCore.DataProtection.Repositories;
using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.ViewModel.Emp;

namespace PaymentSystem.Models
{
    public class Emp
    {
        private IEmpRepository _empRepository;

        public Emp(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }
        public void AddEmp(AddEmpViewModel addEmpViewModel)
        {
            var empDbModel = new EmpDbModel
            {
                EmpId = addEmpViewModel.EmpId,
                Name = addEmpViewModel.Name,
                Address = addEmpViewModel.Address
            };

            _empRepository.AddEmp(empDbModel);
        }
    }
}
