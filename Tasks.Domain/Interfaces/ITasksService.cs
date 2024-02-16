namespace Tasks.Domain.Interfaces;

public interface ITasksService
{
    Task<ObjectResult> CreateTasksAsync(CreateTasksRequest command);

    Task<ObjectResult> UpdatedTasksAsync(UpdatedTasksRequest command);
    
    Task<ObjectResult> UpdateStatusAsync(PatchTasksRequest command);

    Task<ObjectResult> FindByIdAsync(GetTasksByIdQuery query);

    Task<ObjectResult> GetAllTasksAsync(GetTasksQuery query);

    Task<ObjectResult> DeleteLogicalByIdAsync(DeleteTasksRequest command);
}