using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .UseWindowsService()
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                     var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                          .AddJsonFile("appsettings.json", true, true)
                                                          .AddJsonFile($"appsettings.{envName}.json", true, true)
                                                          .AddEnvironmentVariables()
                                                          .Build();
                     webBuilder.UseConfiguration(config)
                               .UseStartup<Startup>();
                 });
    }
}
