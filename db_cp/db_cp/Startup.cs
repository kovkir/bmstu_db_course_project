using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using db_cp.Models;
using db_cp.Services;
using db_cp.Interfaces;
using db_cp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace db_cp
{
    public class Startup
    {
        //private IConfigurationRoot _configuration;

        //public Startup(IWebHostEnvironment hostEnv)
        //{
        //    _configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        //}

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration["DatabaseConnection"] = configuration.GetConnectionString("guestConnection");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDBContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<AppDBContext>(options => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDBContext>(options =>
                options.UseNpgsql(Configuration["DatabaseConnection"]),
                ServiceLifetime.Transient);

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ICoachService, CoachService>();
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<ISquadService, SquadService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ICoachRepository, CoachRepository>();
            services.AddTransient<IClubRepository, ClubRepository>();
            services.AddTransient<ISquadRepository, SquadRepository>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Logout}/{id?}");
            });
        }
    }
}
