using Moq;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Factories;
using Shouldly;
using Xunit;

namespace MyToDo.Tests.UnitTests.Entities;

public sealed class CommentTests
{
    private readonly Mock<IDateTimeService> _dateTimeOffsetProviderMock;

    public CommentTests()
    {
        _dateTimeOffsetProviderMock = new Mock<IDateTimeService>();
    }

    // [Fact]
    // public void UpdateText_Should_UpdateTextAndLastUpdatedOn()
    // {   
    //     // Arrange
    //     var comment = CreateDefaultComment();
    //     var updatedText = Guid.NewGuid().ToString();
    //     var lastUpdatedOn = DateTimeOffset.UtcNow;
    //
    //     _dateTimeOffsetProviderMock.Setup(x => x.UtcNow)
    //         .Returns(lastUpdatedOn);
    //
    //     // Act
    //     comment.UpdateText(updatedText);
    //     
    //     // Assert
    //     comment.Text.ShouldBe(updatedText);
    //     // comment.LastUpdatedOn.ShouldBe(lastUpdatedOn);
    // }
    //
    // private Comment CreateDefaultComment()
    // {
    //     return CommentFactory.Create(
    //         Guid.NewGuid().ToString(),
    //         Guid.NewGuid(),
    //         MemberFactory.Create(Guid.NewGuid().ToString(),
    //             Guid.NewGuid().ToString(),
    //             Guid.NewGuid().ToString(),
    //             Guid.NewGuid().ToString()).Value.TaskId).Value;
    // }
}
