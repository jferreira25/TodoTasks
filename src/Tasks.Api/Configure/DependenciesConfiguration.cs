using Tasks.Domain.Interfaces;
using Tasks.Domain.MappersProfiles;
using Tasks.Domain.Services;

namespace Tasks.Api.Configure;

public static class DependenciesConfiguration
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<ITasksRepository>(mongo => new TasksRepository(configuration["ConnectionsString:MongoDB"], configuration["MongoDB:DatabaseName"], "Task"));
        services.AddSingleton<ITasksService, TasksService>();
        services.AddAutoMapper(typeof(TaskProfile));
    }
}