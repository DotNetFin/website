using Hangfire;
using Hangfire.LiteDB;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using website.Services.EmailSender;
using website.Services.GitHubService;
using website.Services.NotifictionService;
using website.Services.StatisticService;

namespace website;

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
        services.AddResponseCaching();
        services.AddTransient((provider) => new LiteDatabase($"Filename=database.db; Mode=Shared;"));
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == Environments.Development)
        {
            Console.WriteLine("This is Development environment");
            services.AddTransient<IEmailSender, ConsoleEmailSender>();
        }
        else
        {
            Console.WriteLine("This is Production environment");
            services.AddTransient<IEmailSender, SendgridEmailSender>();

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });
        }
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IStatisticService, StatisticService>();
        services.AddTransient<GitHubService>();
        services.AddHttpClient();

        services.AddHangfire(p => p.UseLiteDbStorage("LiteDBHangfire.db"));
        services.AddHangfireServer();
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

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseResponseCaching();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });

        app.UseHangfireDashboard();

        #region background jobs
        RecurringJob.AddOrUpdate<GitHubService>(
            service => service.SetProjects(),
            "0 */4 * * *");

        RecurringJob.AddOrUpdate<IStatisticService>(
            service => service.SetMembersCount(),
            "0 */23 * * *"
        );

        RecurringJob.AddOrUpdate<IStatisticService>(
            service => service.SetMembersCitiesCount(),
            "0 */23 * * *"
        );
        #endregion

    }
}
