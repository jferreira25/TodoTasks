namespace Tasks.Domain.Services;

public sealed class TasksService : ITasksService
{
    private readonly ILogger<TasksService> _logger;
    private readonly ITasksRepository _tasksRepository;
    private readonly IMapper _mapper;

    public TasksService(
        ILogger<TasksService> logger,
        ITasksRepository tasksRepository,
        IMapper mapper)
    {
        _tasksRepository = tasksRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ObjectResult> CreateTasksAsync(CreateTasksRequest command)
    {
        _logger.LogInformation($"{nameof(TasksService)}.CreateTasksAsync Start created task.");

        var entity = _mapper.Map<Entity.Tasks>(command);
        entity.SetCreatedDate();

        await _tasksRepository.AddAsync(entity);

        var responseTask = new CreateTaskResponse()
        {
            Data = new CreateTaskResponse.TaskResponse(entity.Id, entity.CreatedDate.Value)
        };

        _logger.LogInformation($"{nameof(TasksService)}.CreateTasksAsync End created task.");

        return new ObjectResult(responseTask) { StatusCode = StatusCodes.Status201Created };
    }

    public async Task<ObjectResult> UpdatedTasksAsync(UpdatedTasksRequest command)
    {
        _logger.LogInformation($"{nameof(TasksService)}.UpdatedTasksAsync Start updated task.");

        var createdTask = await _tasksRepository.FindOneAsync(x => x.Id == command.Id);

        if (!(createdTask is null))
        {
            UpdatedEntityProperties(command, createdTask);

            await _tasksRepository.UpdateAsync(createdTask);

            return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status204NoContent };
        }

        _logger.LogInformation($"{nameof(TasksService)}.UpdatedTasksAsync task not found.");

        return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status404NotFound };
    }

    public async Task<ObjectResult> UpdateStatusAsync(PatchTasksRequest command)
    {
        _logger.LogInformation($"{nameof(TasksService)}.UpdateStatusAsync Start updated status task.");

        var task = await _tasksRepository.FindOneAsync(x => x.Id == command.Id);

        if (!(task is null))
        {
            task.Status = command.Status;

            CreatedCompletedDate(task);

            await _tasksRepository.UpdateAsync(task);

            return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status204NoContent };
        }

        _logger.LogInformation($"{nameof(TasksService)}.UpdateStatusAsync task not found.");

        return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status404NotFound };
    }

    public async Task<ObjectResult> FindByIdAsync(Guid id)
    {
        _logger.LogInformation($"{nameof(TasksService)}.FindByIdAsync Start find task by id.");

        var task = await _tasksRepository.FindOneAsync(x => x.Id == id);

        if (task is null)
        {
            _logger.LogInformation($"{nameof(TasksService)}.FindByIdAsync task not found.");

            return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status404NotFound };
        }

        var response = new GetTasksByIdQueryResponse()
        {
            Data = _mapper.Map<GetTasksByIdQueryResponse.TaskResponseById>(task)
        };

        _logger.LogInformation($"{nameof(TasksService)}.FindByIdAsync End find task by id.");

        return new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<ObjectResult> GetAllTasksAsync(GetTasksQuery query)
    {
        _logger.LogInformation($"{nameof(TasksService)}.GetAllTasksAsync Start get all tasks.");

        IEnumerable<Entity.Tasks> tasks;
        var filterDate = query?.StartDate is null || query?.EndDate is null;

        if (filterDate)
            tasks = await _tasksRepository.WhereAsync(task => task.Deleted == !query.Active);
        else
            tasks = await _tasksRepository.WhereAsync(task => task.Deleted == !query.Active &&
                                                        task.CreatedDate >= query.StartDate &&
                                                        task.CreatedDate <= query.EndDate);

        if (!(tasks?.Any() ?? false))
        {
            _logger.LogInformation($"{nameof(TasksService)}.GetAllTasksAsync task not found.");

            return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status404NotFound };
        }

        var response = new GetTasksQueryResponse()
        {
            Data = _mapper.Map<List<GetTasksQueryResponse.TaskResponse>>(tasks)
        };

        _logger.LogInformation($"{nameof(TasksService)}.GetAllTasksAsync End get all tasks.");

        return new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<ObjectResult> DeleteLogicalByIdAsync(Guid id)
    {
        _logger.LogInformation($"{nameof(TasksService)}.DeleteLogicalByIdAsync Start delete task.");

        var task = await _tasksRepository.FindOneAsync(x => x.Id == id);

        if (task is null)
        {
            _logger.LogInformation($"{nameof(TasksService)}.DeleteLogicalByIdAsync task not found.");

            return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status404NotFound };
        }
        task.SetDelete();

        await _tasksRepository.UpdateAsync(task);

        _logger.LogInformation($"{nameof(TasksService)}.DeleteLogicalByIdAsync End delete task.");

        return new ObjectResult(string.Empty) { StatusCode = StatusCodes.Status204NoContent };
    }

    private void UpdatedEntityProperties(
        UpdatedTasksRequest command,
        Entity.Tasks createdTask)
    {
        createdTask.Status = command.Status;
        createdTask.Title = command.Title;
        createdTask.Body = command.Body;

        CreatedCompletedDate(createdTask);
    }

    private void CreatedCompletedDate(Entity.Tasks task)
    {
        if (task.Status == TasksStatus.Closed)
            task.CompletedDate = DateTime.Now;
    }
}