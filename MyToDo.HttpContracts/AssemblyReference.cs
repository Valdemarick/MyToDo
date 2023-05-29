using System.Reflection;

namespace MyToDo.HttpContracts;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
