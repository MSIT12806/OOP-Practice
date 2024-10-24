﻿using PaymentSystem.Controllers;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public static class PaydayMapper
    {
        public static EmpSalaryCore ToCore(SalarySaveViewModel vm)
        {
            return new EmpSalaryCore(vm.EmpId, vm.Salary, vm.PayWay);
        }

        public static SalarySaveViewModel ToSaveViewModel(EmpSalaryCore core)
        {
            return new SalarySaveViewModel
            {
                EmpId = core.EmpId,
                Salary = core.Amount,
                PayWay = core.PayWay
            };
        }
    }
}
