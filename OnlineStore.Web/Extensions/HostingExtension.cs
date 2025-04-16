using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using OnlineStore.Application.Static;
using OnlineStore.Data.Context;
using OnlineStore.Ioc.Container;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace OnlineStore.Web.Extensions
{
    public static class HostingExtension
    {
        public static WebApplication RegisterServices(this WebApplicationBuilder builder)
        {

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            #region Config Job

            builder.Services.RegisterJobs();

            #endregion

            #region Config Services

            builder.Services.RegisterService();
            builder.Services.AddHttpClient();


            #endregion

            #region Config db context

            string connectionString = builder.Configuration.GetConnectionString("OnlineStoreConnection");

            builder.Services.AddDbContext<OnlineStoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            #endregion

            #region Config MeliSms
            builder.Configuration.GetSection("MeliSms").Get<MeliSms>();
            #endregion

            #region Config SocialMedia
            builder.Configuration.GetSection("SocialMedia").Get<SocialMedia>();
            #endregion

            #region HtmlEncoder
            builder.Services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
        UnicodeRanges.Arabic }));

            #endregion

            #region Authentication

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                });

            #endregion

            var app = builder.Build();

            return app; 

        }

        public static WebApplication RegisterPipeLines(this WebApplication app)
        {
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/error/404");
            app.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            return app;
        }
    }
}
