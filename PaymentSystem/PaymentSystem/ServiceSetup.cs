using Microsoft.EntityFrameworkCore;
using PaymentSystem.Adapter;
using PaymentSystem.Adapter.Payday;
using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;

public static class ServiceSetup
{

    public static void RegisterService(WebApplicationBuilder builder)
    {
        // 加入 Entity Framework Core 服務(需要 using Microsoft.EntityFrameworkCore 以及 Microsoft.EntityFrameworkCore.InMemory)
        builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemoryDb"));

        // 加入 Emp 相關服務
        builder.Services.AddTransient<IEmpRepository, EmpRepository>();
        builder.Services.AddTransient<EmpService, EmpService>();

        // 加入 ServiceCharge 相關服務
        builder.Services.AddTransient<ServiceChargeService, ServiceChargeService>();
        builder.Services.AddTransient<IServiceChargeRepository, ServiceChargeRepository>();
        builder.Services.AddTransient<IEmpExistChecker, EmpExistChecker>();

        // 加入 Payday 相關服務
        builder.Services.AddTransient<IPaydayRepository, PaydayRepository>();
        builder.Services.AddTransient<IServiceChargeSetter, ServiceChargeSetter>();
        builder.Services.AddTransient<ISalesReceiptSetter, SalesReceiptSetter>();
        builder.Services.AddTransient<PaydayService, PaydayService>();
    }
}


#endregion