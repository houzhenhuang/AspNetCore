using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ContactManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var userPassword = configuration["SeedUserPassword"];
                    var context = services.GetRequiredService<Data.ApplicationDbContext>();
                    context.Database.Migrate();
                    Data.ApplicationSeedData.Initialize(services, userPassword).Wait();
                }
                catch (Exception ex)
                {
                    throw new System.Exception("You need to update the DB "
                        + "\nPM > Update-Database " + "\n or \n" +
                          "> dotnet ef database update"
                          + "\nIf that doesn't work, comment out SeedData and "
                          + "register a new user");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
