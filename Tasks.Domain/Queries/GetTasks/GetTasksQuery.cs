namespace Tasks.Domain.Queries.GetTasks;

public class GetTasksQuery
{
    public GetTasksQuery(
        bool active,
        DateTime? startDate,
        DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        Active = active;
    }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool Active { get; set; }
}