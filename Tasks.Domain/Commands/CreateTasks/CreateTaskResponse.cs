namespace Tasks.Domain.Commands.CreateTasks;

public class CreateTaskResponse : ResponseBase<CreateTaskResponse.TaskResponse>
{
    public sealed class TaskResponse
    {
        public TaskResponse(
            Guid id,
            DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }

        public Guid Id { get; set; }
    }
}