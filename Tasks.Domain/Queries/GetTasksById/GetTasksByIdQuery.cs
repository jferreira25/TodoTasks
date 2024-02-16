namespace Tasks.Domain.Queries.GetTasksById;

public class GetTasksByIdQuery
{
    public GetTasksByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}