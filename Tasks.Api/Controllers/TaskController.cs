using Microsoft.AspNetCore.Mvc;
using Tasks.Domain.Commands.CreateTasks;
using Tasks.Domain.Commands.DeleteTasks;
using Tasks.Domain.Commands.PatchTasks;
using Tasks.Domain.Commands.UpdatedTasks;
using Tasks.Domain.Interfaces;
using Tasks.Domain.Queries.GetTasks;
using Tasks.Domain.Queries.GetTasksById;

namespace Tasks.Api.Controllers;

[Route("api/v1/tasks")]
public class TasksController : Controller
{
    private readonly ITasksService _tasksService;
    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpPost]
    [ProducesResponseType<CreateTaskResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostAsync([FromBody] CreateTasksRequest createTasksCommand)
    {
        var result = await _tasksService.CreateTasksAsync(createTasksCommand);
        return Created($"{nameof(PostAsync)}", result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdatedTasksRequest updatedTasksCommand)
    {
        updatedTasksCommand.Id = id;

        return await _tasksService.UpdatedTasksAsync(updatedTasksCommand);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchAsync(Guid id, [FromBody] PatchTasksRequest PatchTaskscommand)
    {
        PatchTaskscommand.Id = id;

        return await _tasksService.UpdateStatusAsync(PatchTaskscommand);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(Guid id)
    => await _tasksService.FindByIdAsync(new GetTasksByIdQuery(id));

    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(bool active , DateTime? startDate = null, DateTime? endDate = null)
    => await _tasksService.GetAllTasksAsync(new GetTasksQuery(active, startDate, endDate));

    [HttpDelete("{id}")]
    [ProducesResponseType<string>(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    => await _tasksService.DeleteLogicalByIdAsync(new DeleteTasksRequest(id));

}