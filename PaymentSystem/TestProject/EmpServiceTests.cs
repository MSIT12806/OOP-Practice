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
         * [v] AcceptanceMounthlyPaymentTest
         * [] AcceptanceHourlyPaymentTest
         * [] AcceptanceMounthlySwitchToHourlyTest
         * [] AcceptanceHourlySwitchToMounthlyTest
         */
        [Test]
        public void AcceptanceMounthlyPaymentTest()
        {
            /*
             最複雜的操作案例：
            [v] 添加員工
            [v] 修改員工姓名
            [v] 設定員工薪資《時薪》
            [v] 修改員工薪資
            [v] 設定員工公會服務費
            [v] 刪除員工公會服務費
            [v] 重新設定員工公會服務費
            [v] 再加入一筆公會服務費
            [v] 加入一筆銷售收據
            [v] 刪除一筆銷售收據
            [v] 重新加入一筆銷售收據
            [v] 再加入一筆銷售收據
            [v] 薪水結算
             */

            // empService
            var empService = _serviceProvider.GetRequiredService<EmpService>();

            // 添加員工
            var employee = empService.Build("AA","Jane Doe", "123 Main St");
            if (ASSERT)
            {
                // 確認員工是否成功添加
                var emp = empService.Rebuild(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // 修改員工姓名
            employee.UpdateName( "Jane Smith");
            if (ASSERT)
            {
                // 確認修改是否正確
                var emp = empService.Rebuild(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            // 設定員工薪資
            employee.SetSalary(1000, EmpSalaryCore.PayWayEnum.Monthly);
            if (ASSERT)
            {
                // 確認薪資是否正確
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(1000));
            }

            // 修改員工薪資
            employee.SetSalary(2000, EmpSalaryCore.PayWayEnum.Monthly);
            if (ASSERT)
            {
                // 確認修改是否正確
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(2000));
            }

            // 設定員工公會服務費
            string setviceChargeId = employee.AddServiceCharge(employee.Id, 100, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            if (ASSERT)
            {
                // 確認服務費是否正確
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp.Amount, Is.EqualTo(100));
            }

            // 刪除員工公會服務費
            employee.DeleteServiceCharge(setviceChargeId);
            if (ASSERT)
            {
                // 確認刪除是否正確
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp, Is.EqualTo(null));
            }


            // 重新設定員工公會服務費
            // 再加入一筆公會服務費
            setviceChargeId = employee.AddServiceCharge(employee.Id, 200, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            setviceChargeId = employee.AddServiceCharge(employee.Id, 300, DateOnly.FromDateTime(new DateTime(2021, 1, 5)));
            if (ASSERT)
            {
                // 確認服務費數目是否
                var chargeServices = employee.GetServiceCharge();
                Assert.That(chargeServices.Count(), Is.EqualTo(2));
            }

            // 加入一筆銷售收據
           string salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                100
            );
            if (ASSERT)
            {
                // 確認銷售收據是否正確
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(1));
            }

            // 刪除一筆銷售收據
            employee.DeleteSalesReceiptBy(salesReceiptId);

            // 重新加入一筆銷售收據
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                200
            );

            // 再加入一筆銷售收據
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 5)),
                300
            );

            if (ASSERT)
            {
                // 確認銷售收據數目是否正確
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(2));
            }

            // paydayService
            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();

            // 薪水結算
            var paydays = paydayService.Pay(DateOnly.FromDateTime(new DateTime(2021, 1, 30)));

            if (ASSERT)
            {
                // 確認薪水結算是否正確
                Assert.That(paydays.Count(), Is.EqualTo(1));
                Assert.That(paydays.First().EmpId, Is.EqualTo(employee.Id));
                Assert.That(paydays.First().Salary, Is.EqualTo(2000));
                Assert.That(paydays.First().ServiceCharge, Is.EqualTo(500));
                Assert.That(paydays.First().SalesReceipt, Is.EqualTo(500));
                Assert.That(paydays.First().ShouldPay, Is.EqualTo(2000));
            }
        }

        [Test]
        public void AcceptanceHourlyPaymentTest()
        {

            /*
             最複雜的操作案例：
            [] 添加員工
            [] 修改員工姓名
            [] 設定員工薪資《時薪》
            [] 修改員工薪資
            [] 設定員工公會服務費
            [] 刪除員工公會服務費
            [] 重新設定員工公會服務費
            [] 再加入一筆公會服務費
            [] 加入一筆銷售收據
            [] 刪除一筆銷售收據
            [] 重新加入一筆銷售收據
            [] 再加入一筆銷售收據
            [] 再加入一筆出缺勤紀錄
            [] 再刪除一筆出缺勤紀錄
            [] 再加入一筆出缺勤紀錄
            [] 薪水結算
             */


            // empService
            var empService = _serviceProvider.GetRequiredService<EmpService>();

            // 添加員工
            var employee = empService.Build("AA", "Jane Doe", "123 Main St");
            if (ASSERT)
            {
                // 確認員工是否成功添加
                var emp = empService.Rebuild(employee.Id);
                Assert.That(emp.Id, Is.EqualTo(employee.Id));
            }

            // 修改員工姓名
            employee.UpdateName("Jane Smith");
            if (ASSERT)
            {
                // 確認修改是否正確
                var emp = empService.Rebuild(employee.Id);
                Assert.That(emp.Name, Is.EqualTo("Jane Smith"));
            }

            // 設定員工薪資
            employee.SetSalary(10, EmpSalaryCore.PayWayEnum.Hourly);
            if (ASSERT)
            {
                // 確認薪資是否正確
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(10));
                Assert.That(salary.PayWay, Is.EqualTo(EmpSalaryCore.PayWayEnum.Hourly));
            }

            // 修改員工薪資
            employee.SetSalary(20, EmpSalaryCore.PayWayEnum.Hourly);
            if (ASSERT)
            {
                // 確認修改是否正確
                var salary = employee.GetSalary();
                Assert.That(salary.Amount, Is.EqualTo(20));
            }

            // 設定員工公會服務費
            // TODO: 這邊要透過建模的方式，自動去限制不能為 時薪員工設定公會服務費
            string setviceChargeId = employee.AddServiceCharge(employee.Id, 100, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            if (ASSERT)
            {
                // 確認服務費是否正確
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp.Amount, Is.EqualTo(100));
            }

            // 刪除員工公會服務費
            employee.DeleteServiceCharge(setviceChargeId);
            if (ASSERT)
            {
                // 確認刪除是否正確
                ServiceChargeCore emp = employee.GetServiceChargeBy(setviceChargeId);
                Assert.That(emp, Is.EqualTo(null));
            }


            // 重新設定員工公會服務費
            // 再加入一筆公會服務費
            setviceChargeId = employee.AddServiceCharge(employee.Id, 200, DateOnly.FromDateTime(new DateTime(2021, 1, 1)));
            setviceChargeId = employee.AddServiceCharge(employee.Id, 300, DateOnly.FromDateTime(new DateTime(2021, 1, 5)));
            if (ASSERT)
            {
                // 確認服務費數目是否
                var chargeServices = employee.GetServiceCharge();
                Assert.That(chargeServices.Count(), Is.EqualTo(2));
            }

            // 加入一筆銷售收據
            string salesReceiptId = employee.AddSalesReceipt(
                 employee.Id,
                 DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                 100
             );
            if (ASSERT)
            {
                // 確認銷售收據是否正確
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(1));
            }

            // 刪除一筆銷售收據
            employee.DeleteSalesReceiptBy(salesReceiptId);

            // 重新加入一筆銷售收據
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 1)),
                200
            );

            // 再加入一筆銷售收據
            salesReceiptId = employee.AddSalesReceipt(
                employee.Id,
                DateOnly.FromDateTime(new DateTime(2021, 1, 5)),
                300
            );

            if (ASSERT)
            {
                // 確認銷售收據數目是否正確
                IEnumerable<SalesReceiptCore> salesReceipts = employee.GetSalesReceipts();
                Assert.That(salesReceipts.Count(), Is.EqualTo(2));
            }

            // paydayService
            var paydayService = _serviceProvider.GetRequiredService<PaydayService>();

            // 薪水結算
            var paydays = paydayService.Pay(DateOnly.FromDateTime(new DateTime(2021, 1, 30)));

            if (ASSERT)
            {
                // 確認薪水結算是否正確
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