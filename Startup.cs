using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project_66_bit.Models;
using Project_66_bit.Services.Auth;
using Project_66_bit.Services.ReportService;

namespace Project_66_bit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAntiforgery(option => {
                option.HeaderName = "XSRF-TOKEN";
                option.SuppressXFrameOptionsHeader = false;
            });
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizePage("/Index");
                options.Conventions.AuthorizePage("/Mod");
                options.Conventions.AddAreaPageRoute("Page", "/Index", "");
            });
            services.AddControllersWithViews();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connection)
            );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Enter");
                });

            services.AddTransient<Authentication>();
            services.AddTransient<ReportService>();
            services.AddTransient<IProjectConverter, ExcelConverter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseStatusCodePagesWithRedirects("/Error?code={0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
