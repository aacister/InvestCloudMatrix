
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using InvestCloudMatrix.Console.Interfaces;
using InvestCloudMatrix.Console.Proxies;
using InvestCloudMatrix.Console.Presenters;
using InvestCloudMatrix.Console;


var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

var host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration(builder =>
{
    builder.Sources.Clear();
    builder.AddConfiguration(configuration);
})
.ConfigureServices(services =>
{

    services.AddSingleton<IInvestCloudMatrixProxies, InvestCloudMatrixProxies>();
    services.AddScoped<IInvestCloudMatrixPresenters, InvestCloudMatrixPresenters>();
    services.AddSingleton<IApplication, Application>();

})
.ConfigureLogging(logging =>
{
    logging.AddConsole();
})
.Build();

var app = host.Services.GetRequiredService<IApplication>();
await app.Run();

