using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Web.Components;

public abstract class BaseComponent : ComponentBase
{
    protected static Error Error = null!;

    protected static bool IsShowErrorDialog;
    
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

    protected string GetErrorTextInRussian()
    {
        if (Error == DomainErrors.Member.MemberNotFound)
        {
            return "Такой пользователь не найден";
        }
        
        if (Error == DomainErrors.Forbidden)
        {
            return "У вас нет разрешения на это действие. Обратитесь к администратору";
        }

        return "";
    }
}