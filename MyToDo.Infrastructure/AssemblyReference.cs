using System.Reflection;

namespace MyToDo.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Infrastructure.AssemblyReference).Assembly;
}
