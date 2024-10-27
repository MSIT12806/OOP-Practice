using Microsoft.EntityFrameworkCore;
using PaymentSystem.Adapter;
using PaymentSystem.Adapter.BasicDataMaintenence;
using PaymentSystem.Adapter.Payment;
using PaymentSystem.Application;
using PaymentSystem.Models.BasicDataMaintenece;
using PaymentSystem.Models.Payment;

public static class ServiceSetup
{

    public static void RegisterService(IServiceCollection services)
    {
        // 加入 Entity Framework Core 服務(需要 using Microsoft.EntityFrameworkCore 以及 Microsoft.EntityFrameworkCore.InMemory)
        services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemoryDb"),ServiceLifetime.Scoped, ServiceLifetime.Scoped);

        // 加入 Emp 相關服務
        services.AddScoped<IEmpDataRepository, EmpDataRepository>();
        services.AddScoped<EmpDataService, EmpDataService>();

        // 加入 Payment 相關服務
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        // 加入 ServiceCharge 相關服務

        // 加入 Payday 相關服務
    }
}


