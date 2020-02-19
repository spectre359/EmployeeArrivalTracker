﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Reporting.Data.Context;
using Reporting.Repositories;
using Reporting.Repositories.Interfaces;
using Reporting.Services;
using Reporting.Services.Interfaces;

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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region Dependencies
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IWebServiceSettings>(x => Configuration.GetSection("Settings").Get<Settings>());

            AddRefitService<IWebService>(services, Configuration.GetSection("Settings:WebServiceBaseUrl").Value);
            services.AddScoped<IWebServiceManager, WebServiceManager>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeArrivalRepository, EmployeeArrivalRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            services.AddDbContext<ReportingToolContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("ReportingDB")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
    }
}