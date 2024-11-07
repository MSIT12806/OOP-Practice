using IdentityCoreModule;
using IdentityModule;
using IdentityModule.Implement;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Adapter.IdentityValidation;

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
            IdentityService.InitialServices<IdentityDataAccess>(builder.Services);
            IdentityService.RegisterIdentity(typeof(HRForId), new HRPolicy());

            //// 加入各種服務End

            #region 基礎設定

            var app = builder.Build();
            InitialDatas(app);

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

            IdentityService.Pipeline(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();


            #endregion
        }
        private static void InitialDatas(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var user = new AspNetUser
                {
                    Id = "1",
                    Name = "Robin",
                    PasswordHash = new EncodeHelper().HashPassword("P@ssw0rd", "Robin".ToUpper(), "IdentityV2")
                };
                dbContext.DefaultUsers.Add(user);

                var role = new AspNetRole
                {
                    Id = "1",
                    Name = nameof(HRForId)
                };
                dbContext.DefaultRoles.Add(role);

                var userRole = new AspNetUserRole
                {
                    Id = "1",
                    UserId = user.Id,
                    RoleName = role.Name
                };
                dbContext.DefaultUserRoles.Add(userRole);

                dbContext.SaveChanges();
            }


        }
    }
}
