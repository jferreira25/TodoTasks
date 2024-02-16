public class GetTasksByIdQueryResponse : ResponseBase<GetTasksByIdQueryResponse.TaskResponseById>
{
    public sealed class TaskResponseById
    {
        public Guid Id { get; set; }
        
        public string Body { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? CompletedDate { get; set; }

        public TasksStatus Status { get; set; }
    }
}