using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;
using PaymentSystem.Models;
using System.ComponentModel.Design;

namespace TestProject
{
    public class EmpServiceTests
    {
        private IServiceProvider _serviceProvider;

        const bool ASSERT = true;

        [SetUp]
        public void Setup()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ServiceSetup.RegisterService(services);
                })
                .Build();

            _serviceProvider = host.Services;
        }

        [Test]
        public void AddAndModifyEmployee_Test()
        {
            // empService
            var empService = _serviceProvider.GetRequiredService<EmpService>();

            // 添加員工
            var employee = new EmpCore
            {
                Id = "AA",
                Name = "Jane Doe",
                Address = "123 Main St"
            };
            empService.AddEmp(employee);

            if(ASSERT)
            {
                // 確認員工是否成功添加
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // 修改員工
            employee.Name = "Jane Smith";
            empService.ChgEmp(employee);

            if(ASSERT)
            {
                // 確認修改是否正確
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            // paydayService
            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();

            // 設定員工薪資
            var salary = new EmpSalaryCore
            {
                EmpId = employee.Id,
                Salary = 1000
            };
            paydayService.SetSalary(salary);

            if (ASSERT)
            {
                // 確認薪資是否正確
                var emp = paydayService.GetEmpSalary(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(1000));
            }

            // 修改員工薪資
            salary.Salary = 2000;
            paydayService.SetSalary(salary);
            if (ASSERT)
            {
                // 確認修改是否正確
                var emp = paydayService.GetEmpSalary(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(2000));
            }

            // chargeService
            var chargeService = _serviceProvider.GetRequiredService<ServiceChargeService>();

            // 設定員工公會服務費
            var serviceCharge = new ServiceChargeCore
            {
                EmpId = employee.Id,
                MemberId = "AA",
                Amount = 100
            };
            chargeService.SetServiceCharge(serviceCharge);

            if (ASSERT)
            {
                // 確認服務費是否正確
                var emp = chargeService.GetSingle(employee.Id);
                Assert.That(emp.Amount, Is.EqualTo(100));
            }

            // 修改員工公會服務費
            serviceCharge.Amount = 200;
            chargeService.SetServiceCharge(serviceCharge);

            if (ASSERT)
            {
                // 確認修改是否正確
                var emp = chargeService.GetSingle(employee.Id);
                Assert.That(emp.Amount, Is.EqualTo(200));
            }

            // 薪水結算
            var paydays = paydayService.Pay();

            if (ASSERT)
            {
                // 確認薪水結算是否正確
                Assert.That(paydays.Count(), Is.EqualTo(1));
                Assert.That(paydays.First().EmpId, Is.EqualTo(employee.Id));
                Assert.That(paydays.First().Salary, Is.EqualTo(2000));
                Assert.That(paydays.First().ServiceCharge, Is.EqualTo(200));
                Assert.That(paydays.First().SalesReceipt, Is.EqualTo(null));
                Assert.That(paydays.First().ShouldPay, Is.EqualTo(1800));
            }
        }
    }
}