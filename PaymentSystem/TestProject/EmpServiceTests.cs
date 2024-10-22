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
            [v] �A�[�J�@�����|�A�ȶO
            [v] �[�J�@���P�⦬��
            [v] �R���@���P�⦬��
            [v] ���s�[�J�@���P�⦬��
            [v] �A�[�J�@���P�⦬��
            [v] �~������
             */

            // empService
            var empService = _serviceProvider.GetRequiredService<EmpService>();
            var empRepository = _serviceProvider.GetRequiredService<IEmpRepository>();

            // �K�[���u
            var employee = empService.InstantiationEmp("AA","Jane Doe", "123 Main St");
            empService.AddEmp(employee);
            if (ASSERT)
            {
                // �T�{���u�O�_���\�K�[
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // �ק���u�m�W
            employee.UpdateName( "Jane Smith");
            if (ASSERT)
            {
                // �T�{�ק�O�_���T
                var emp = empService.GetSingle(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            // �]�w���u�~��
            employee.SetSalary(1000, EmpSalaryCore.PayWayEnum.Monthly);
            if (ASSERT)
            {
                // �T�{�~��O�_���T
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(1000));
            }

            // �ק���u�~��
            employee.SetSalary(2000, EmpSalaryCore.PayWayEnum.Monthly);
            if (ASSERT)
            {
                // �T�{�ק�O�_���T
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(2000));
            }

            // �]�w���u���|�A�ȶO
            string setviceChargeId = employee.AddServiceCharge(employee.Id, 100, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            if (ASSERT)
            {
                // �T�{�A�ȶO�O�_���T
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp.Amount, Is.EqualTo(100));
            }

            // �R�����u���|�A�ȶO
            employee.DeleteServiceCharge(setviceChargeId);
            if (ASSERT)
            {
                // �T�{�R���O�_���T
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp, Is.EqualTo(null));
            }


            // ���s�]�w���u���|�A�ȶO
            // �A�[�J�@�����|�A�ȶO
            setviceChargeId = employee.AddServiceCharge(employee.Id, 200, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            setviceChargeId = employee.AddServiceCharge(employee.Id, 300, DateOnly.FromDateTime(new DateTime(2021, 1, 5)));
            if (ASSERT)
            {
                // �T�{�A�ȶO�ƥجO�_
                var chargeServices = employee.GetServiceCharge();
                Assert.That(chargeServices.Count(), Is.EqualTo(2));
            }

            // �[�J�@���P�⦬��
           string salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                100
            );
            if (ASSERT)
            {
                // �T�{�P�⦬�ڬO�_���T
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(1));
            }

            // �R���@���P�⦬��
            employee.DeleteSalesReceiptBy(salesReceiptId);

            // ���s�[�J�@���P�⦬��
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                200
            );

            // �A�[�J�@���P�⦬��
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 5)),
                300
            );

            if (ASSERT)
            {
                // �T�{�P�⦬�ڼƥجO�_���T
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(2));
            }

            // paydayService
            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();

            // �~������
            var paydays = paydayService.Pay(DateOnly.FromDateTime(new DateTime(2021, 1, 30)));

            if (ASSERT)
            {
                // �T�{�~������O�_���T
                Assert.That(paydays.Count(), Is.EqualTo(1));
                Assert.That(paydays.First().EmpId, Is.EqualTo(employee.Id));
                Assert.That(paydays.First().Salary, Is.EqualTo(2000));
                Assert.That(paydays.First().ServiceCharge, Is.EqualTo(500));
                Assert.That(paydays.First().SalesReceipt, Is.EqualTo(500));
                Assert.That(paydays.First().ShouldPay, Is.EqualTo(2000));
            }
        }
    }
}