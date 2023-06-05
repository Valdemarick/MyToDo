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

    protected override async Task OnInitializedAsync()
    {
        await GetTagPageAsync();
    }

    private async Task GetTagPageAsync()
    {
        var memberPage = await TagService.GetPageAsync(_parameters);
        if (memberPage.IsFailure) return;
        _tags = memberPage.Value.Items;
        _pageView = memberPage.Value.PageView;
    }

    private async Task DeleteTag()
    {
        await TagService.DeleteAsync(_tagIdToDelete);

        await CloseDeleteDialog();
    }
    
    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }
    
    private async Task GetMemberPageAsync()
    {
        var memberPage = await TagService.GetPageAsync(_parameters);
        if (memberPage.IsFailure) return;
        _tags = memberPage.Value.Items;
        _pageView = memberPage.Value.PageView;
    }

    private void ShowCreateDialog() => _isShowCreateDialog = true;

    private async Task CloseCreateDialog()
    {
        await GetTagPageAsync();

        _isShowCreateDialog = false;
    }

    private void ShowUpdateDialog(TagDto tag)
    {
        _tagToUpdate = tag;
        _isShowUpdateDialog = true;
    }

    private async Task CloseUpdateDialog()
    {
        await GetTagPageAsync();
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
        await GetTagPageAsync();

        _isShowDeleteDialog = false;
    }
}
