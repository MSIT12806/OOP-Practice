using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Payment.Application;
using System.Net;

namespace TestProject
{
    /// <summary>
    ///關於 支付薪水 領域層的測試
    /// </summary>
    public class PaymentDomainTests
    {
        const bool ASSERT = true;
        const bool ARRANGE = true;

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
        public async Task TestHourlyEmployeeSettle()
        {
            /*
             * [v]新增一個員工
             * 設定給薪方式為時薪
             * 
             * 設定建檔日期為 2024/1/1
             * 設定時薪為 100 元
             * 
             * 提交工作出勤卡：
             * 工作日期：2024/1/2，工作時數：8 小時，加班時數：2 小時
             * 工作日期：2024/1/3，工作時數：8 小時，加班時數：2 小時
             * 工作日期：2024/1/4，工作時數：8 小時，加班時數：2 小時
             * 
             * 切換到付款日期：2024/1/5
             * 執行付款，並進行驗證
             */


            // 新增一個時薪員工
            await using (var s = this._serviceProvider.CreateAsyncScope())
            {
                var empService = s.ServiceProvider.GetRequiredService<EmpDataService>();
                empService.Build(empId: "AA", name: "Jane Doe", address: "123 Main St");
            }
            if (ASSERT)
            {
                await using (var s = this._serviceProvider.CreateAsyncScope())
                {
                    // 確認員工是否成功添加
                    var empService = s.ServiceProvider.GetRequiredService<EmpDataService>();
                    var emp = empService.Rebuild("AA");
                    Assert.That(emp.Id, Is.EqualTo(emp.Id));
                }
            }
        }
    }
}