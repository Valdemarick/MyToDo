using System.Reflection;

namespace MyToDo.WebApi;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
