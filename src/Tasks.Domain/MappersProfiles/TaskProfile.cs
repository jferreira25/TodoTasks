namespace Tasks.Domain.MappersProfiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTasksRequest, Entity.Tasks>(MemberList.None);
        
    }
}