using LH.Tool.Decoupling;
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
        /*
         * [_] AcceptanceMounthlyPaymentTest
         * [] AcceptanceHourlyPaymentTest
         */
        [Test]
        public void AcceptanceMounthlyPaymentTest()
        {
            /*
             �̽������ާ@�רҡG
            [v] �K�[���u
            [v] �ק���u�m�W
            [v] �]�w���u�~��m���~�n
            [v] �ק���u�~��
            [v] �]�w���u���|�A�ȶO
            [v] �R�����u���|�A�ȶO
            [v] ���s�]�w���u���|�A�ȶO
            [] �A�[�J�@�����|�A�ȶO
            [] �[�J�@���P�⦬��
            [] �R���@���P�⦬��
            [] ���s�[�J�@���P�⦬��
            [] �A�[�J�@���P�⦬��
            [v] �~������
             */

            // empService
            var empService = _serviceProvider.GetRequiredService<EmpService>();

            // �K�[���u
            var employee = new EmpCore
            {
                Id = "AA",
                Name = "Jane Doe",
                Address = "123 Main St"
            };
            empService.AddEmp(employee);
            if (ASSERT)
            {
                // �T�{���u�O�_���\�K�[
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // �ק���u�m�W
            employee.Name = "Jane Smith";
            empService.ChgEmp(employee);
            if (ASSERT)
            {
                // �T�{�ק�O�_���T
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            // paydayService
            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();

            // �]�w���u�~��
            var salary = new EmpSalaryCore
            {
                EmpId = employee.Id,
                Salary = 1000
            };
            paydayService.SetSalary(salary);
            if (ASSERT)
            {
                // �T�{�~��O�_���T
                var emp = paydayService.GetEmpSalary(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(1000));
            }

            // �ק���u�~��
            salary.Salary = 2000;
            paydayService.SetSalary(salary);
            if (ASSERT)
            {
                // �T�{�ק�O�_���T
                var emp = paydayService.GetEmpSalary(employee.Id);
                Assert.That(emp.Salary, Is.EqualTo(2000));
            }

            // chargeService
            var chargeService = _serviceProvider.GetRequiredService<ServiceChargeService>();

            // �]�w���u���|�A�ȶO
            var setviceChargeId = chargeService.AddServiceCharge(employee.Id, 100, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            if (ASSERT)
            {
                // �T�{�A�ȶO�O�_���T
                var emp = chargeService.GetSingle(setviceChargeId);
                Assert.That(emp.Amount, Is.EqualTo(100));
            }

            // �R�����u���|�A�ȶO
            chargeService.DeleteServiceCharge(setviceChargeId);
            if (ASSERT)
            {
                // �T�{�R���O�_���T
                var emp = chargeService.GetSingle(setviceChargeId);
                Assert.That(emp, Is.EqualTo(null));
            }


            // ���s�]�w���u���|�A�ȶO
            setviceChargeId = chargeService.AddServiceCharge(employee.Id, 200, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            if (ASSERT)
            {
                // �T�{�A�ȶO�ƥجO�_
                var chargeServices = chargeService.GetListBy(employee.Id);
                Assert.That(chargeServices.Count(), Is.EqualTo(1));
            }

            // �~������
            var paydays = paydayService.Pay(DateOnly.FromDateTime(new DateTime(2021, 1, 30)));

            if (ASSERT)
            {
                // �T�{�~������O�_���T
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