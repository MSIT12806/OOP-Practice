using Microsoft.EntityFrameworkCore;
using PaymentSystem.Adapter;
using PaymentSystem.Adapter.Payday;
using PaymentSystem.Application.Emp;
using PaymentSystem.Application.Payday;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//// 加入各種服務Start

// 加入 Entity Framework Core 服務(需要 using Microsoft.EntityFrameworkCore 以及 Microsoft.EntityFrameworkCore.InMemory)
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemoryDb"));

// 加入 Emp 相關服務
builder.Services.AddTransient<IEmpRepository, EmpRepository>();
builder.Services.AddTransient<EmpService, EmpService>();

// 加入 ServiceCharge 相關服務
builder.Services.AddTransient<IServiceChargeRepository, ServiceChargeRepository>();
builder.Services.AddTransient<IEmpExistChecker, EmpExistChecker>();
builder.Services.AddTransient<ServiceChargeService, ServiceChargeService>();

// 加入 Payday 相關服務
builder.Services.AddTransient<IServiceChargeGetter, ServiceChargeGetter>();
builder.Services.AddTransient<IPaydayRepository, PaydayRepository>();
builder.Services.AddTransient<PaydayService, PaydayService>();

//// 加入各種服務End

#region 基礎設定

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion