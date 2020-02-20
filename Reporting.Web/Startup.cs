using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Reporting.Contracts.Employee;
using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories;
using Reporting.Repositories.Interfaces;
using Reporting.Services;
using Reporting.Services.Interfaces;
using Serilog;
using System;

namespace Reporting.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {               
                options.Cookie.IsEssential = true;
            });

            ConfigureAutoMapper();

            #region Dependencies
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IWebServiceSettings>(x => Configuration.GetSection("Settings").Get<Settings>());
            services.AddSingleton<ISettings>(x => Configuration.GetSection("Settings").Get<Settings>());

            AddRefitService<IWebService>(services, Configuration.GetSection("Settings:WebServiceBaseUrl").Value);
            services.AddScoped<IWebServiceManager, WebServiceManager>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeArrivalRepository, EmployeeArrivalRepository>();
            services.AddScoped<IEmployeeArrivalsService, EmployeeArrivalsService>();
            services.AddScoped<IHistoryEventRepository, HistoryEventRepository>();
            services.AddScoped<IHistoryEventsService, HistoryEventsService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(CreateLogger());
            #endregion

            services.AddDbContext<ReportingToolContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("ReportingDB")));

            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });
          
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddSessionStateTempDataProvider();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           
        }

        private void AddRefitService<TClient>(IServiceCollection services, string baseAddress) where TClient : class
        {
            services.AddHttpClient<TClient>(c =>
            {
                c.BaseAddress = new Uri(baseAddress);
            })
            .AddTypedClient(c => RestService.For<TClient>(c));
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {                
                cfg.CreateMap<EmployeeArrival, EmployeeArrivalRequest>().ReverseMap();
            });
        }

        private ILogger CreateLogger()
        {
            var configuration = new LoggerConfiguration();

            configuration = configuration.MinimumLevel.Debug();

            configuration = configuration.WriteTo.RollingFile("_log-{Date}.txt", fileSizeLimitBytes: (100 * 1024 * 1024));


            return configuration
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Reporting.Web")

                .CreateLogger();
        }

        public static Action<IApplicationBuilder> HandleException(IApplicationBuilder app)
        {
            return appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var logger = app.ApplicationServices.GetService(typeof(ILogger)) as ILogger;
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    logger.Error(exceptionHandlerFeature.Error.Message);
                    context.Response.Redirect($"/Home/Error/{context.Response.StatusCode}/{exceptionHandlerFeature.Error.Message}");
                });
            };
        }
    }
}
