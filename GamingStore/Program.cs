using GamingStore.Areas.Admin.Services.Admin;
using GamingStore.Areas.Admin.Services.Blogs;
using GamingStore.Data;
using GamingStore.Services.Games;
using GamingStore.Services.Home;
using GamingStore.Services.Publishers;
using GamingStore.Services.Reviews;
using GamingStore.Services.Sellers;
using GamingStore.Services.ShoppingCart;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<GamingStoreDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GamingStoreDbContext>();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<IHomeService, HomeService>();
            builder.Services.AddTransient<ISellerService, SellerService>();
            builder.Services.AddTransient<IReviewService, ReviewService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<IPublisherService, PublisherService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IBlogService, BlogService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage().UseMigrationsEndPoint();
            else app.UseExceptionHandler("/Home/Error").UseHsts();

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "/{controller=Home}/{action=Index}/{id?}/{area?}");

                endpoints.MapControllerRoute(
                    name: "Game Details",
                    pattern: "/Games/Details/{id}/{information}",
                    defaults: new { Controller = "Games", action = "Details" });

                endpoints.MapRazorPages();
            });

            app.Run();
        }
    }
}