using IjarifySystemBLL.Services.Classes;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Repositories.Classes;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IjarifySystemPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPropertyService,PropertyService>();
            builder.Services.AddScoped<IPropertyRepository,PropertyRepository>();
            //builder.Services.AddScoped<IReviewService,ReviewService>();

            builder.Services.AddDbContext<IjarifyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
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
        }
    }
}
