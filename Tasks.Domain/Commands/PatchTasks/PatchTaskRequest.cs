using System.Text.Json.Serialization;

namespace Tasks.Domain.Commands.PatchTasks;

public class PatchTasksRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    public TasksStatus Status { get; set; }
}