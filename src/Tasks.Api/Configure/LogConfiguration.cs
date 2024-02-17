using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace Tasks.Api.Configure;

public static class LogConfiguration
{
    public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithMachineName()  
                    .Enrich.WithThreadId()
                    .Enrich.WithThreadName()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .CreateLogger();
        services.AddSerilog(Log.Logger);

    }
}