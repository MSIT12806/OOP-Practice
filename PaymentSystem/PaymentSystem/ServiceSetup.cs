using LH.Tool.Decoupling;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Adapter;
using PaymentSystem.Adapter.Payday;
using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;

public static class ServiceSetup
{

    public static void RegisterService(IServiceCollection services)
    {
        // 註冊 DateProvider
        services.AddSingleton<DateProvider>();

        // 加入 Entity Framework Core 服務(需要 using Microsoft.EntityFrameworkCore 以及 Microsoft.EntityFrameworkCore.InMemory)
        services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemoryDb"));

        // 加入 Emp 相關服務
        services.AddTransient<IEmpRepository, EmpRepository>();
        services.AddTransient<EmpService, EmpService>();

        // 加入 ServiceCharge 相關服務
        services.AddTransient<ServiceChargeService, ServiceChargeService>();
        services.AddTransient<IServiceChargeRepository, ServiceChargeRepository>();
        services.AddTransient<IEmpExistChecker, EmpExistChecker>();

        // 加入 Payday 相關服務
        services.AddTransient<IPaydayRepository, PaydayRepository>();
        services.AddTransient<IServiceChargeSetter, ServiceChargeSetter>();
        services.AddTransient<ISalesReceiptSetter, SalesReceiptSetter>();
        services.AddTransient<PaydayService, PaydayService>();
    }
}


