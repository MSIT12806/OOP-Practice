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

            // �K�[���u
            var employee = new EmpCore
            {
                Id = "AA",
                Name = "Jane Doe",
                Address = "123 Main St"
            };
            empService.AddEmp(employee);

            if(ASSERT)
            {
                // �T�{���u�O�_���\�K�[
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // �ק���u
            employee.Name = "Jane Smith";
            empService.ChgEmp(employee);

            if(ASSERT)
            {
                // �T�{�ק�O�_���T
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();
            // �K�[���u�~��
            var salary = new EmpSalaryCore
            {
                EmpId = employee.Id,
                Salary = 1000
            };
            paydayService.SaveSalary(salary);

            if (ASSERT)
            {
                // �T�{�~��O�_���T
                var emp = paydayService.GetSingle(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(1000));
            }
        }
    }
}