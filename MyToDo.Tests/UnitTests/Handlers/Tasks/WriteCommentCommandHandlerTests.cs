using Moq;
using MyToDo.Application.CQRS.Tasks.Commands.WriteComment;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using Shouldly;
using Xunit;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Tests.UnitTests.Handlers.Tasks;

public sealed class WriteCommentCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDateTimeOffsetProvider> _dateTimeOffsetProviderMock;

    public WriteCommentCommandHandlerTests()
    {
        _taskRepositoryMock = new();
        _memberRepositoryMock = new();
        _unitOfWorkMock = new();
        _dateTimeOffsetProviderMock = new();
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldBeSuccess()
    {
        // Arrange
        var task = CreateDefaultTask();
        var member = CreateDefaultMember();

        var lastUpdatedOn = DateTimeOffset.UtcNow;

        var command = new WriteCommentCommand(
            Guid.NewGuid().ToString(),
            task.Id,
            member.Id);

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(id => id == task.Id),
                It.IsAny<CancellationToken>(), It.Is<bool>(isTracking => isTracking)))
            .ReturnsAsync(task);

        _memberRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(id => id == member.Id),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        _dateTimeOffsetProviderMock.Setup(x => x.UtcNow)
            .Returns(lastUpdatedOn);

        var handler = new WriteCommentCommandHandler(
            _taskRepositoryMock.Object,
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeOffsetProviderMock.Object);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        task.Comments.Count.ShouldBe(1);
        task.LastUpdatedOn.ShouldBe(lastUpdatedOn);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_WhenTaskNotFound_ShouldBeFailure()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var member = CreateDefaultMember();

        var command = new WriteCommentCommand(
            Guid.NewGuid().ToString(),
            taskId,
            member.Id);

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>(), It.IsAny<bool>()))
            .ReturnsAsync((Task)null!);

        _memberRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(id => id == member.Id),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var handler = new WriteCommentCommandHandler(
            _taskRepositoryMock.Object,
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeOffsetProviderMock.Object);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainErrors.Task.TaskNotFound);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_WhenMemberNotFound_ShouldBeFailure()
    {
        // Arrange
        var task = CreateDefaultTask();
        var memberId = Guid.NewGuid();

        _taskRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(id => id == task.Id),
                It.IsAny<CancellationToken>(), It.Is<bool>(isTracking => isTracking)))
            .ReturnsAsync(task);

        _memberRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member)null!);

        var command = new WriteCommentCommand(Guid.NewGuid().ToString(),
            task.Id,
            memberId);

        var commandHandler = new WriteCommentCommandHandler(
            _taskRepositoryMock.Object,
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeOffsetProviderMock.Object);
        
        // Act
        var result = await commandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainErrors.Member.MemberNotFound);
    }

    private Task CreateDefaultTask()
    {
        return Task.Create(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Priority.Normal,
            TaskType.Task,
            Guid.NewGuid(),
            Guid.NewGuid());
    }

    private Member CreateDefaultMember()
    {
        return Member.Create(Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString());
    }
}