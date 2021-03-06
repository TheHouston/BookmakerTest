﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Bookmaker
{
    public class Program
    {
        public static void Main(string[] args) =>
          CreateWebHostBuilder(args)
          .Build()
          .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                     logging.AddDebug();
                 })
                 .UseKestrel()
                 .UseIISIntegration();
    }
}
