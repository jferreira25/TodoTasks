using System.Text.Json.Serialization;

namespace Tasks.Domain.Commands.UpdatedTasks;

public class UpdatedTasksRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    public string Body { get; set; }

    public string Title { get; set; }

    public TasksStatus Status { get; set; }
}