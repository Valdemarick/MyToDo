using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Shared;

namespace MyToDo.Web.Components;

public abstract class BaseComponent : ComponentBase
{
    protected Error Error = null!;

    protected bool IsShowErrorDialog;
    
    protected void ShowErrorDialog(Error error)
    {
        Error = error;
        IsShowErrorDialog = true;
    }

    protected void CloseErrorDialog()
    {
        Error = null!;
        IsShowErrorDialog = false;
    }
}