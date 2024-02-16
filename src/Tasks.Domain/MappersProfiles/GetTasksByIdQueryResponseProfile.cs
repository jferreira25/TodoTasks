namespace Tasks.Domain.MappersProfiles;

public class GetTasksByIdQueryResponseProfile : Profile
{
    public GetTasksByIdQueryResponseProfile()
    {
        CreateMap<Entity.Tasks, GetTasksByIdQueryResponse.TaskResponseById>(MemberList.None);
    }
}