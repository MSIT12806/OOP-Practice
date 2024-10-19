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

            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();
            // 添加員工薪資
            var salary = new EmpSalaryCore
            {
                EmpId = employee.Id,
                Salary = 1000
            };
            paydayService.SaveSalary(salary);

            if (ASSERT)
            {
                // 確認薪資是否正確
                var emp = paydayService.GetSingle(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(1000));
            }
        }
    }
}