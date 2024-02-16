using Tasks.Domain.Interfaces;
using Tasks.Infrastructure.Repositories;

public class TasksRepository : MongoBaseRepository<Tasks.Domain.Entities.Tasks, Guid>, ITasksRepository
{
    public TasksRepository(
        string connection,
        string databaseName,
        string collectionName,
        string partitionKey = null)
        : base(connection, databaseName, collectionName, partitionKey)
    {
    }
}