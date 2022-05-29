using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StandardApi.Data;
using System.Threading.Tasks;

namespace StandardApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                bool isAdminRoleExists = await roleManager.RoleExistsAsync("Admin");
                if (!isAdminRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                bool isPosterRoleExists = await roleManager.RoleExistsAsync("Poster");
                if (!isPosterRoleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Poster"));
                }

                var userManager = serviceScope
                    .ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                var testUser = await userManager.FindByEmailAsync("test_sqli@example.com");

                await userManager.AddToRoleAsync(testUser, "Admin");

                var poster = await userManager.FindByEmailAsync("poster2@example.com");
                await userManager.AddToRoleAsync(poster, "Poster");

                var superadmin = await userManager.FindByEmailAsync("superadmin@example.com");
                await userManager.AddToRoleAsync(superadmin, "Poster");
                await userManager.AddToRoleAsync(superadmin, "Admin");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
