namespace Tasks.Domain.Commands.CreateTasks;

public class CreateTaskResponse : ResponseBase<CreateTaskResponse.TaskResponse>
{
    public sealed class TaskResponse
    {
        public TaskResponse(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}