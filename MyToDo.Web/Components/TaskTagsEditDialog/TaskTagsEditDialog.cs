using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyToDo.HttpContracts.Tags;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Components.TaskTagsEditDialog;

public partial class TaskTagsEditDialog
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter]
    public Guid TaskId { get; set; }

    private readonly LinkTagsToTaskDto _linkTagsToTaskDto = new();

    private List<TagDto> _tags = new();

    private List<TagDto> _linkedTags = new();

    private List<Guid> _tagsToLinkIds = new();

    protected override async Task OnInitializedAsync()
    {
        var getTagsResult = await TagService.GetAllAsync();
        if (getTagsResult.IsFailure)
        {
            ShowErrorDialog(getTagsResult.Error);
            return;
        }

        _tags = getTagsResult.Value;

        var getLinkedTagsResult = await TaskService.GetLinkedTagsAsync(TaskId);
        if (getLinkedTagsResult.IsFailure)
        {
            ShowErrorDialog(getTagsResult.Error);
            return;
        }

        _linkedTags = getLinkedTagsResult.Value;
    }

    private async Task LinkAsync()
    {
        _linkTagsToTaskDto.TaskId = TaskId;
        _linkTagsToTaskDto.TagIds = _tagsToLinkIds;

        var linkResult = await TaskService.LinkTagsToTaskAsync(_linkTagsToTaskDto);
        if (linkResult.IsFailure)
        {
            ShowErrorDialog(linkResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }

    private void UpdateLinkedTagsList(ChangeEventArgs args)
    {
        if (args.Value is not string[] listOfStrings)
        {
            return;
        }

        List<Guid> ids = new();

        foreach (var @string in listOfStrings)
        {
            if (Guid.TryParse(@string, out var id))
            {
                ids.Add(id);
            }
        }
        
        _tagsToLinkIds.Clear();
        _tagsToLinkIds.AddRange(ids);
    }
}