using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineBooking.Helpers;
using Dapper.FastCrud;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OnlineBooking
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Ayarlar classı yapılandırması:
            services.AddOptions();
            services.Configure<VeriTabani>(Configuration.GetSection("VeriTabani"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/AnaSayfa/Hata");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Ohb",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/Kullanici/Giris"),
                LogoutPath = new PathString("/Kullanici/Cikis"),
                AccessDeniedPath = new PathString("/Kullanici/Izin"),
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=AnaSayfa}/{action=Index}/{id?}");
            });

            ConfigureDataBase();
        }

        private void ConfigureDataBase()
        {
            // Dapper.FastCRUD
            var providerName = Configuration["VeriTabani:ProviderName"];

            var validDialect = Enum.TryParse<SqlDialect>(providerName, out SqlDialect dialect);
            if (!validDialect)
                throw new Exception($"'{ providerName }' is not a valid dialect name.");

            OrmConfiguration.DefaultDialect = dialect;
        }
    }
}
