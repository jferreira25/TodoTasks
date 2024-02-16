namespace Tasks.Domain.MappersProfiles;

public class GetTasksQueryResponseProfile : Profile
{
    public GetTasksQueryResponseProfile()
    {
        CreateMap<Entity.Tasks, GetTasksQueryResponse.TaskResponse>(MemberList.None);
    }
}