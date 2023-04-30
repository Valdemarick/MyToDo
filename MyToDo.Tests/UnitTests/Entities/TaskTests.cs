using Moq;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
using Shouldly;
using Xunit;
using Task = MyToDo.Domain.Entities.Task;
using TaskFactory = MyToDo.Domain.Factories.TaskFactory;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Tests.UnitTests.Entities;

public sealed class TaskTests
{
    private readonly Mock<IDateTimeOffsetProvider> _dateTimeProviderMock;

    public TaskTests()
    {
        _dateTimeProviderMock = new Mock<IDateTimeOffsetProvider>();
    }

    // [Fact]
    // public void StartWorkingOnTask_WhenTaskIsOpen_StatusShouldBecomeInProgress()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     var lastUpdateOn = SetupDateTimeProvider();
    //     
    //     // Act
    //     var result = task.StartWorkOnTask(_dateTimeProviderMock.Object);
    //     
    //     //Assert
    //     result.IsSuccess.ShouldBeTrue();
    //     task.Status.ShouldBe(TaskStatus.InProgress);
    //     task.LastUpdatedOn.ShouldBe(lastUpdateOn);
    // }
    //
    // [Fact]
    // public void StartWorkingOnTask_WhenTaskInCompleted_ShouldBeFailure()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     task.Complete(_dateTimeProviderMock.Object);
    //     var lastUpdateOn = SetupDateTimeProvider();
    //
    //     // Act
    //     var result = task.StartWorkOnTask(_dateTimeProviderMock.Object);
    //     
    //     // Assert
    //     result.IsFailure.ShouldBeTrue();
    //     result.Error.ShouldBe(DomainErrors.Task.TaskIsCompleted);
    //     
    //     task.LastUpdatedOn.ShouldNotBe(lastUpdateOn);
    // }
    //
    // [Fact]
    // public void StartWorkingOnTask_WhenTaskIsAlreadyInProgress_ShouldBeFailure()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     var lastUpdateOn = SetupDateTimeProvider();
    //     
    //     task.StartWorkOnTask(_dateTimeProviderMock.Object);
    //     
    //     // Act
    //     var result = task.StartWorkOnTask(_dateTimeProviderMock.Object);
    //     
    //     // Assert
    //     result.IsFailure.ShouldBeTrue();
    //     result.Error.ShouldBe(DomainErrors.Task.TaskIsAlreadyInProgress);
    //     task.LastUpdatedOn.ShouldBe(lastUpdateOn);
    // }
    //
    // [Fact]
    // public void CompleteTask_WhenTaskIsNotCompletedYet_ShouldCompleteTask()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     var completedOn = SetupDateTimeProvider();
    //     
    //     // Act
    //     var result = task.Complete(_dateTimeProviderMock.Object);
    //     
    //     // Assert
    //     result.IsSuccess.ShouldBeTrue();
    //     task.Status.ShouldBe(TaskStatus.Completed);
    //     task.CompletedOn.ShouldBe(completedOn);
    // }
    //
    // [Fact]
    // public void CompleteTask_WhenTaskIsAlreadyCompleted_ShouldBeFailure()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     var completedOn = SetupDateTimeProvider();
    //     
    //     task.Complete(_dateTimeProviderMock.Object);
    //     _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.Now.AddHours(1));
    //     
    //     // Act
    //     var result = task.Complete(_dateTimeProviderMock.Object);
    //     
    //     // Assert
    //     result.IsFailure.ShouldBeTrue();
    //     result.Error.ShouldBe(DomainErrors.Task.TaskIsAlreadyCompleted);
    //     
    //     task.CompletedOn.ShouldBe(completedOn);
    // }
    //
    // [Fact]
    // public void AssignTask_TaskShouldHaveNewExecutor()
    // {
    //     // Arrange
    //     var task = CreateDefaultTask();
    //     var lastUpdateOn = SetupDateTimeProvider();
    //     
    //     var executor = TaskExecutorFactory.Create(Guid.NewGuid().ToString(),
    //         Guid.NewGuid()).Value;
    //     
    //     // Act
    //     task.Assign(executor);
    //     
    //     // Assert
    //     task.Executor.ShouldBe(executor);
    //     // task.LastUpdatedOn.ShouldBe(lastUpdateOn);
    // }
    //
    // private Task CreateDefaultTask()
    // {
    //     return TaskFactory.Create(
    //         Guid.NewGuid().ToString(),
    //             Guid.NewGuid().ToString(),
    //             Priority.Normal,
    //             TaskType.Task,
    //         Guid.NewGuid(),
    //             Guid.NewGuid()).Value;
    // }
    //
    // private DateTimeOffset SetupDateTimeProvider()
    // {
    //     var completedOn = DateTimeOffset.UtcNow;
    //
    //     _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(completedOn);
    //
    //     return completedOn;
    // }
}
