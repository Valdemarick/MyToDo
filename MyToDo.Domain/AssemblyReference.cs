using System.Reflection;

namespace MyToDo.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Domain.AssemblyReference).Assembly;
}
