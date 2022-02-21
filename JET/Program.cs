using JET;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace JET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var configurationRoot = config.Build();

                Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(configurationRoot.GetConnectionString("DefaultConnection"),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    AutoCreateSqlTable = true,
                    TableName = "Logs"
                })
                .CreateLogger();
            })
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}