using AutoMapper;
using Tasks.Domain.MappersProfiles;

public static class MappersMock
{
    public static IMapper GetMock()
    {
        var mapperConfigurationExpression = new MapperConfigurationExpression();

        mapperConfigurationExpression.AddProfile<GetTasksByIdQueryResponseProfile>();
        mapperConfigurationExpression.AddProfile<GetTasksQueryResponseProfile>();
        mapperConfigurationExpression.AddProfile<TaskProfile>();

        var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
        mapperConfiguration.AssertConfigurationIsValid();

        return new Mapper(mapperConfiguration);
    }
}
