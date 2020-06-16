using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;

namespace FlashMoney
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateWebHostBuilder(args).Build();
            //var env = host.Services.GetRequiredService<IHostingEnvironment>();
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration).CreateLogger();

            //try
            //{
            //    Log.Information("Application started");
            //    host.Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Application failed to start");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
            .ConfigureServices(servicecollection => servicecollection
            .Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "azure-diagnistics-";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 14;

            })
            .Configure<AzureBlobLoggerOptions>(options =>
            {
                options.BlobName = "log.txt";
            }))
                //.UseSerilog()
                .UseStartup<Startup>();
    }
}
