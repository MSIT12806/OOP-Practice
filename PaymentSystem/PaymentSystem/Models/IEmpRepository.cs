

using PaymentSystem.Models.BasicDataMaintenece;

namespace PaymentSystem.Models
{
    public interface IEmpRepository 
    {
        void Build(string empId, string name, string address);
        Employee Rebuild(string empId);
        void ChgEmpName(string empId, string name);
        void ChgEmpAddress(string empId, string address);
        IEnumerable<string> GetEmpIds();


    }
}
