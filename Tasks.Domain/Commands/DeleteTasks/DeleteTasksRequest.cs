namespace Tasks.Domain.Commands.DeleteTasks;

public class DeleteTasksRequest
{
    public DeleteTasksRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}