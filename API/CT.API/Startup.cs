using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using CT.API.Logging;

using DATA = CT.Data;
using REPO = CT.Repo;
using ABS = CT.Service.Abstract;
using SVC = CT.Service;
using Swashbuckle.AspNetCore.Swagger;

namespace CT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DATA.CTDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("Allow_CT_NG_Origin",
                    builder => builder.WithOrigins("http://localhost:4200"));
            });

            services.AddMvc();

            // Add repositories services.
            services.AddScoped(typeof(REPO.IRepository<>), typeof(REPO.Repository<>));

            services.AddTransient<ABS.IOwnerService, SVC.OwnerService>();
            services.AddTransient<ABS.IPetService, SVC.PetService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DATA.CTDbContext cTDbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(Configuration.GetConnectionString("LoggingConnection"), LogLevel.Information);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCors("Allow_CT_NG_Origin");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DATA.DbInitializer.Initialize(cTDbContext);

        }
    }
}
