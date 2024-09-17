using TimeApp.Models;
using Microsoft.Extensions.Logging;
using TimeZone.Models;

namespace TimeApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<TimeProps>();
            builder.Services.AddTransient<TimeZoneNames>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=TimeZone}/{action=Index}/{id?}");

            builder.Logging.AddConsole();

            app.Run();
        }
    }
}