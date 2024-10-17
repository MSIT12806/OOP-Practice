using PaymentSystem.Application.Emp;

namespace PaymentSystem.Adapter
{
    public class EmpExistChecker : IEmpExistChecker
    {
        private AppDbContext _context;

        public EmpExistChecker(AppDbContext context)
        {
            _context = context;
        }
        public bool Check(string empId)
        {
            return _context.Emps.Any(x => x.EmpId == empId);
        }
    }
}
