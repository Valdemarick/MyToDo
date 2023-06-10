using Microsoft.AspNetCore.Components;
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
        
    }
}