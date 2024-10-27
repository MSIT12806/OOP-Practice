using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem
{
    public class Program
    {
        public static bool IsDevelopment { get; set; } = true;
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(o =>
            {
                if (IsDevelopment)
                {
                    // 使用防偽造中介軟體
                    o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }
            });


            //// 加入各種服務Start

            ServiceSetup.RegisterService(builder.Services);

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
        }
    }
}
