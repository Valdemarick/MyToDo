using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Components.UpdateTag;

public partial class UpdateTag
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter]
    public TagDto Tag { get; set; }

    private UpdateTagDto _updateTagDto = new();

    protected override void OnInitialized()
    {
        _updateTagDto.Id = Tag.Id;
        _updateTagDto.Name = Tag.Name;
    }

    private async Task UpdateAsync()
    {
        var result = await TagService.UpdateAsync(_updateTagDto);
        if (result.IsFailure)
        {
            ShowErrorDialog(result.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
}