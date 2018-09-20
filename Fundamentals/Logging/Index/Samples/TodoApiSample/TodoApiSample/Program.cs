#define FilterFunction

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.Console;
namespace TodoApiSample
{
    public class Program
    {
#if TemplateCode
        #region TemplateCode
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        #endregion
#elif ExpandDefault
        #region ExpandDefault
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();  //将日志信息输出到控制台
                    logging.AddDebug();  //将日志信息输出到输出调试中
                })
                .UseStartup<Startup>()
                .Build();
            webHost.Run();
        }
        #endregion
#elif Scopes
        #region Scopes
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole(options => options.IncludeScopes = true);
                    logging.AddDebug();
                })
                .UseStartup<Startup>()
                .Build();
            webHost.Run();
        }
        #endregion
#elif FilterInCode
        #region FilterInCode
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
        
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                    logging.AddFilter("System", LogLevel.Debug)
                          // .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Trace))
                              .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace))
                .Build();
        #endregion
#elif MinLevel
        #region MinLevel
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            //当配置文件中没有设置时，些设置生效
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning))
                .Build();
        #endregion
#elif FilterFunction
        #region FilterFunction
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .ConfigureLogging(logBuilder =>
              {
                  logBuilder.AddFilter((provider, category, logLevel) =>
                  {
                      if (provider == "Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider" &&
                          category == "TodoApiSample.Controllers.TodoController")
                      {
                          return false;
                      }
                      return true;
                  });
              })
              .Build();
        #endregion
#endif
    }
}
