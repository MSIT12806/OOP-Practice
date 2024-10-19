using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using PaymentSystem.Application.Emp;
using PaymentSystem.Models;
using System.ComponentModel.Design;

namespace TestProject
{
    public class EmpServiceTests
    {
        private IServiceProvider _serviceProvider;

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

            // 修改員工
            employee.Name = "Jane Smith";
            empService.ChgEmp(employee);

            // 確認修改是否正確
            var updatedEmployee = empService.GetSingle(employee.Id);
            Assert.AreEqual("Jane Smith", updatedEmployee.Name);
        }
    }
}