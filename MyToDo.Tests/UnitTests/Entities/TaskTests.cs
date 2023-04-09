using Microsoft.Extensions.Internal;
using Moq;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using Shouldly;
using Xunit;
using Task = MyToDo.Domain.Entities.Task;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Tests.UnitTests.Entities;

public class TaskTests
{
    private readonly Mock<ISystemClock> _mockClock;

    public TaskTests()
    {
        _mockClock = new Mock<ISystemClock>();
    }

    [Fact]
    public void StartWorkingOnTask_WhenTaskIsOpen_StatusShouldBecomeInProgress()
    {
        // Arrange
        var task = CreateDefaultTask();
        
        // Act
        var result = task.StartWorkOnTask();
        
        //Assert
        result.IsSuccess.ShouldBeTrue();
        task.Status.ShouldBe(TaskStatus.InProgress);
    }

    [Fact]
    public void StartWorkingOnTask_WhenTaskInCompleted_ShouldBeFailure()
    {
        // Arrange
        var task = CreateDefaultTask();
        task.Complete(_mockClock.Object);

        // Act
        var result = task.StartWorkOnTask();
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainErrors.Task.TaskIsCompleted);
    }

    [Fact]
    public void StartWorkingOnTask_WhenTaskIsAlreadyInProgress_ShouldBeFailure()
    {
        // Arrange
        var task = CreateDefaultTask();
        task.StartWorkOnTask();
        
        // Act
        var result = task.StartWorkOnTask();
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainErrors.Task.TaskIsAlreadyInProgress);
    }

    [Fact]
    public void CompleteTask_WhenTaskIsNotCompletedYet_ShouldCompleteTask()
    {
        // Arrange
        var task = CreateDefaultTask();
        var completedOn = DateTimeOffset.UtcNow;

        _mockClock.Setup(x => x.UtcNow).Returns(completedOn);
        
        // Act
        var result = task.Complete(_mockClock.Object);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        task.Status.ShouldBe(TaskStatus.Completed);
        task.CompletedOn.ShouldBe(completedOn);
    }

    [Fact]
    public void CompleteTask_WhenTaskIsAlreadyCompleted_ShouldBeFailure()
    {
        // Arrange
        var task = CreateDefaultTask();
        task.Complete(_mockClock.Object);
        
        // Act
        var result = task.Complete(_mockClock.Object);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainErrors.Task.TaskIsAlreadyCompleted);
    }

    [Fact]
    public void AssignTask_TaskShouldHaveNewExecutor()
    {
        // Arrange
        var task = CreateDefaultTask();

        var executor = Member.Create(Guid.NewGuid());
        
        // Act
        task.Assign(executor);
        
        // Assert
        task.Executor.ShouldBe(executor);
    }

    private Task CreateDefaultTask()
    {
        return Task.Create(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Priority.Normal,
                TaskType.Task,
                DateTime.UtcNow,
                Guid.NewGuid(),
                Guid.NewGuid());
    }
}
