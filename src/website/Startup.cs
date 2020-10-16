using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.LiteDB;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using website.Services.EmailSender;
using website.Services.GitHubService;

namespace website
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
            services.AddRazorPages();
            services.AddTransient((provider) =>
            {
                var path = Configuration.GetConnectionString("LiteDb");
                return new LiteDatabase($"Filename={path}; Mode=Shared;");
            });
            services.AddTransient<IEmailSender, ConsoleEmailSender>();
            services.AddTransient<GitHubService>();
            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                // Github API versioning
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.inertia-preview+json");
                c.DefaultRequestHeaders.Add("User-Agent", "DotNetFin");
            });
            var path = Configuration.GetConnectionString("LiteDBHangfire");
            services.AddHangfire(p => p.UseLiteDbStorage(path));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            #region background jobs
            RecurringJob.AddOrUpdate<GitHubService>(
                service => service.SetProjects(),
                "0 */4 * * *");
            #endregion

        }
    }
}
