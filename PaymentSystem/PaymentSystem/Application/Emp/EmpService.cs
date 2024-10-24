﻿using NuGet.Protocol.Core.Types;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public class EmpService
    {
        private IEmpRepository _empRepository;

        public EmpService(IEmpRepository empRepository)
        {
            this._empRepository = empRepository;
        }

        public Models.Emp Build(string empId, string name, string address)
        {
            var emp = new Models.Emp(empId, this._empRepository);
            emp.InitialData(name, address);
            this._empRepository.Add(emp);
            return emp;
        }

        public Models.Emp Rebuild(string empId)
        {
            return this._empRepository.Rebuild(empId);
        }

        public void ChgEmpName(string empId, string name)
        {
            var emp = _empRepository.Rebuild(empId);
            emp.UpdateName(name);
        }

        public void ChgEmpAddress(string empId, string address)
        {
            var emp = _empRepository.Rebuild(empId);
            emp.UpdateAddress(address);
        }


        public IEnumerable<Models.Emp> GetList()
        {
            return this._empRepository.GetList();
        }
    }
}
