using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace MyToDo.ArchitectureTests;

public sealed class LayerDependencyTests
{
    private static readonly string ApplicationProject = Application.AssemblyReference.Assembly.ToString();
    private static readonly string HttpContractsProject = HttpContracts.AssemblyReference.Assembly.ToString();
    private static readonly string WebProject = Web.AssemblyReference.Assembly.ToString();
    private static readonly string SharedProject = Shared.AssemblyReference.Assembly.ToString();
    private static readonly string InfrastructureProject = Infrastructure.AssemblyReference.Assembly.ToString();
    private static readonly string WebApiProject = WebApi.AssemblyReference.Assembly.ToString();
    private static readonly string PersistenceProject = Persistence.AssemblyReference.Assembly.ToString();
    private static readonly string DomainProject = Domain.AssemblyReference.Assembly.ToString();
    
    [Fact]
    public void DomainLayer_ShouldNot_HaveDependencyOnOtherLayers()
    {
        // Arrange
        var otherLayers = new[]
        {
            ApplicationProject,
            HttpContractsProject,
            WebProject,
            SharedProject,
            InfrastructureProject,
            WebApiProject,
            PersistenceProject,
        };
        
        // Act
        var result = Types
            .InAssembly(Domain.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherLayers)
            .GetResult();
        
        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void PersistenceLayer_ShouldNot_HaveDependencyOnOtherProject_ExceptDomain()
    {
        // Arrange
        var otherLayers = new[]
        {
            ApplicationProject,
            HttpContractsProject,
            WebProject,
            SharedProject,
            InfrastructureProject,
            WebApiProject,
        };
        
        // Act
        var result = Types
            .InAssembly(Persistence.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherLayers)
            .GetResult();
        
        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Controllers_ShouldNot_HaveRepositoriesAsDependency()
    {
        // Act
        var result = Types
            .InAssembly(WebApi.AssemblyReference.Assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .ShouldNot()
            .HaveDependencyOn("MyToDo.Persistence.Repositories")
            .GetResult();
        
        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }
}