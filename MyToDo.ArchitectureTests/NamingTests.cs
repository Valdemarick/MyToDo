using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace MyToDo.ArchitectureTests;

public sealed class NamingTests
{
    [Fact]
    public void CommandHandlerName_ShouldEndWith_CommandHandler()
    {
        // Act
        var result = Types
            .InAssembly(Application.AssemblyReference.Assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();
        
        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void QueryHandlerName_ShouldEndWith_QueryHandler()
    {
        // Act
        var result = Types
            .InAssembly(Application.AssemblyReference.Assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();
        
        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }
}
