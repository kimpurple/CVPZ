using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace CVPZ
{
  public class Program
  {
    public static int Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Debug()
         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
         .Enrich.FromLogContext()
         .WriteTo.Console()
         .CreateLogger();

      try
      {
        Log.Information("Starting web host");
        CreateWebHostBuilder(args).Build().Run();
        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog();
  }
}
