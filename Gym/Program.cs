using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //      
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;

            });
            //

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Manger", "Admin", "Coach", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManger.RoleExistsAsync(role))
                    {
                        await roleManger.CreateAsync(new IdentityRole(role));
                    }
                }

            }

            using (var scope = app.Services.CreateScope())
            {
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var email = "admin@gym.com";
                var password = "@Mm1234";

                if (await userManger.FindByEmailAsync(email) == null)
                {
                    var admin = new ApplicationUser();
                    admin.FirstName = "Admin";
                    admin.LastName = "Admin";
                    admin.PhoneNumber = "00000000000";
                    admin.UserName = email;
                    admin.Email = email;
                    admin.DOB = new DateTime(2003, 10, 26);
                    await userManger.CreateAsync(admin, password);
                    await userManger.AddToRoleAsync(admin, "Admin");

                }
            }

            app.Run();
        }
    }
}