using Microsoft.AspNetCore.Components;
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
        await TagService.UpdateAsync(_updateTagDto);

        await OnClose.InvokeAsync();
    }
}