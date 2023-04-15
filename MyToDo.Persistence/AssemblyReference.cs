using System.Reflection;

namespace MyToDo.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Persistence.AssemblyReference).Assembly;
}
