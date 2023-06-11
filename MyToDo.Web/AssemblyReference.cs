using System.Reflection;

namespace MyToDo.Web;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Web.AssemblyReference).Assembly;
}
