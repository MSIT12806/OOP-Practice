


using System.Collections.Generic;

namespace Payment.Models.BasicDataMaintenece
{
    public interface IEmpDataRepository 
    {
        void Build(string empId, string name, string address);
        Employee Rebuild(string empId);
        void ChgEmpName(string empId, string name);
        void ChgEmpAddress(string empId, string address);
        IEnumerable<string> GetEmpIds();
    }
}
