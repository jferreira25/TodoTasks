using System.Linq.Expressions;
using AutoFixture;
using NSubstitute;
using Tasks.Domain.Interfaces;
using Entity = Tasks.Domain.Entities;
public class TasksRepositoryMock
{
    private readonly ITasksRepository mock = Substitute.For<ITasksRepository>();
    private Fixture _fixture = new Fixture();
    public ITasksRepository GetMock()
    {
        mock.UpdateAsync(Arg.Any<Entity.Tasks>(), Arg.Any<dynamic>());

        mock.FindOneAsync(Arg.Any<Expression<Func<Entity.Tasks, bool>>>()).Returns(_fixture.Create<Entity.Tasks>());

        mock.WhereAsync(Arg.Any<Expression<Func<Entity.Tasks, bool>>>()).Returns(_fixture.CreateMany<Entity.Tasks>(4));

        return mock;
    }

}