using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Pages.Tags;

public partial class Tags
{
    private List<TagDto> _tags = new();
    private PageViewDto _pageView = new PageViewDto();

    private bool _isShowCreateDialog;
    private bool _isShowUpdateDialog;

    private bool _isShowDeleteDialog;

    private readonly TagPageRequestDto _parameters = new TagPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10
    };

    private TagDto _tagToUpdate;

    private Guid _tagIdToDelete;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadDataAsync();
            StateHasChanged();
        }
    }

    private async Task LoadDataAsync()
    {
        var tagPage = await TagService.GetPageAsync(_parameters);
        if (tagPage.IsFailure)
        {
            ShowErrorDialog(tagPage.Error);
            return;
        }
        
        _tags = tagPage.Value.Items;
        _pageView = tagPage.Value.PageView;
    }

    private async Task DeleteTag()
    {
        var deleteResult = await TagService.DeleteAsync(_tagIdToDelete);
        if (deleteResult.IsFailure)
        {
            ShowErrorDialog(deleteResult.Error);
            return;
        }

        await CloseDeleteDialog();
    }
    
    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await LoadDataAsync();
    }

    private void ShowCreateDialog() => _isShowCreateDialog = true;

    private async Task CloseCreateDialog()
    {
        await LoadDataAsync();

        _isShowCreateDialog = false;
    }

    private void ShowUpdateDialog(TagDto tag)
    {
        _tagToUpdate = tag;
        _isShowUpdateDialog = true;
    }

    private async Task CloseUpdateDialog()
    {
        await LoadDataAsync();
        _tagToUpdate = null!;
        _isShowUpdateDialog = false;
    }

    private void ShowDeleteDialog(Guid tagId)
    {
        _tagIdToDelete = tagId;
        _isShowDeleteDialog = true;
    }

    private async Task CloseDeleteDialog()
    {
        await LoadDataAsync();

        _isShowDeleteDialog = false;
    }

    private async Task SearchAsync()
    {
        await LoadDataAsync();
    }
}
