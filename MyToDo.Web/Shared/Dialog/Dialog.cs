using Microsoft.AspNetCore.Components;

namespace MyToDo.Web.Shared.Dialog;

public partial class Dialog
{
    [Parameter] 
    public string Caption { get; set; }
    
    [Parameter] 
    public string Message { get; set; }
    
    [Parameter] 
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter] 
    public Category Type { get; set; }
   
    private Task Cancel()
    {
        return OnClose.InvokeAsync(false);
    }
    private Task Ok()
    {
        return OnClose.InvokeAsync(true);
    }
    public enum Category
    {
        Okay,
        SaveNot,
        DeleteNot
    }
}