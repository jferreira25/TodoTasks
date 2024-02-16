namespace Tasks.Api.Configure;

public static class BuildExtensions
{
    public static void AddBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
    }
}