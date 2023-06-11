using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Components.CreateTag;

public partial class CreateTag
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    private CreateTagDto _createTagDto = new();

    private async Task CreateAsync()
    {
        var result = await TagService.CreateAsync(_createTagDto);
        if (result.IsFailure)
        {
            ShowErrorDialog(result.Error);
            return;
        }
        
        await OnClose.InvokeAsync();
    }
}