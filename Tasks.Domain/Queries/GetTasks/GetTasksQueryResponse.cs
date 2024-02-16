public class GetTasksQueryResponse : ResponseBase<IEnumerable<GetTasksQueryResponse.TaskResponse>>
{
    public sealed class TaskResponse
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? CompletedDate { get; set; }

        public TasksStatus Status { get; set; }
    }
}