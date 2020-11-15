using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using website;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();