using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace MyToDo.Tests.ArchitectureTests;

public class LayerDependencyTests
{
    [Fact]
    public void DomainLayer_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            NamespaceNameConstants.ApplicationNamespace,
            NamespaceNameConstants.InfrastructureNamespace,
            NamespaceNameConstants.PersistenceNamespace,
            NamespaceNameConstants.WebNamespace
        };
        
        // Act
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void PersistenceLayer_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Persistence.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            NamespaceNameConstants.ApplicationNamespace,
            NamespaceNameConstants.InfrastructureNamespace,
            NamespaceNameConstants.WebNamespace
        };
        
        // Act
        var types = Types.InAssembly(assembly);
        
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void InfrastructureLayer_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Persistence.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            NamespaceNameConstants.WebNamespace
        };
        
        // Act
        var types = Types.InAssembly(assembly);
        
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void ApplicationLayer_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Persistence.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            NamespaceNameConstants.WebNamespace,
            NamespaceNameConstants.InfrastructureNamespace,
        };
        
        // Act
        var types = Types.InAssembly(assembly);
        
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.ShouldBeTrue();
    }
}