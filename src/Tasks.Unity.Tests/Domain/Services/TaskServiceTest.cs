using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Tasks.Domain.Commands.CreateTasks;
using Tasks.Domain.Interfaces;
using Tasks.Domain.Services;

namespace Tasks.Unity.Tests.Domain.Services;

public class TaskServiceTest
{
    private TasksService GetService(ITasksRepository tasksRepository = null)
    {
        tasksRepository ??= Substitute.For<ITasksRepository>();

        return new TasksService(
            Substitute.For<ILogger<TasksService>>(),
            tasksRepository,
            MappersMock.GetMock());
    }

    [Fact]
    public async void ShouldBe_CreatedTask()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.CreateTasksAsync(new Tasks.Domain.Commands.CreateTasks.CreateTasksRequest()
        {
            Body = "teste",
            Title = "teste"
        });

        result?.StatusCode.Should().Be(201);

        var resultResponse = (CreateTaskResponse)result?.Value;

        resultResponse.Data.Id.Should().NotBeEmpty();

    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenUpdatedTasks_NotExists()
    {
        var service = GetService();

        var result = await service.UpdatedTasksAsync(new Tasks.Domain.Commands.UpdatedTasks.UpdatedTasksRequest()
        {
            Id = Guid.NewGuid(),
            Body = "Unity",
            Title = "Tests",
            Status = Tasks.Domain.Enums.TasksStatus.Doing
        });

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_UpdatedTasks()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.UpdatedTasksAsync(new Tasks.Domain.Commands.UpdatedTasks.UpdatedTasksRequest()
        {
            Id = Guid.NewGuid(),
            Body = "Unity",
            Title = "Tests",
            Status = Tasks.Domain.Enums.TasksStatus.Doing
        });

        result?.StatusCode.Should().Be(204);
    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenUpdatedStatus_NotExists()
    {
        var service = GetService();

        var result = await service.UpdateStatusAsync(new Tasks.Domain.Commands.PatchTasks.PatchTasksRequest()
        {
            Id = Guid.NewGuid(),
            Status = Tasks.Domain.Enums.TasksStatus.Todo
        });

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_UpdatedStatusTask()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.UpdateStatusAsync(new Tasks.Domain.Commands.PatchTasks.PatchTasksRequest()
        {
            Id = Guid.NewGuid(),
            Status = Tasks.Domain.Enums.TasksStatus.Todo
        });

        result?.StatusCode.Should().Be(204);
    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenDeleteTask_NotExists()
    {
        var service = GetService();

        var result = await service.DeleteLogicalByIdAsync(Guid.NewGuid());

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_DeletedTask()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.DeleteLogicalByIdAsync(Guid.NewGuid());

        result?.StatusCode.Should().Be(204);
    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenTask_NotExists()
    {
        var service = GetService();

        var result = await service.FindByIdAsync(Guid.NewGuid());

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_ReturnOk_WhenTask_Exists()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.FindByIdAsync(Guid.NewGuid());

        result?.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenActiveTasks_NotExists()
    {
        var service = GetService();

        var result = await service.GetAllTasksAsync(new Tasks.Domain.Queries.GetTasks.GetTasksQuery(true, null, null));

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_ReturnNotFound_WhenDesactiveTasks_NotExists()
    {
        var service = GetService();

        var result = await service.GetAllTasksAsync(new Tasks.Domain.Queries.GetTasks.GetTasksQuery(false, null, null));

        result?.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void ShouldBe_ReturnOk_WhenExists_Tasks()
    {
        var service = GetService(new TasksRepositoryMock().GetMock());

        var result = await service.GetAllTasksAsync(new Tasks.Domain.Queries.GetTasks.GetTasksQuery(true, null, null));

        result?.StatusCode.Should().Be(200);
    }
}