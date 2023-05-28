using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Pages.Tags;

public partial class Tags
{
    private List<TagDto> _tags = new();
    private PageViewDto _pageView = new PageViewDto();
    
    private readonly TagPageRequestDto _parameters = new TagPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10
    };

    protected override async Task OnInitializedAsync()
    {
        var memberPage = await TagService.GetPageAsync(_parameters);
        _tags = memberPage.Items;
        _pageView = memberPage.PageView;
    }

    private async Task DeleteTag(Guid id)
    {
        await TagService.DeleteAsync(id);
    }
    
    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }
    
    private async Task GetMemberPageAsync()
    {
        var memberPage = await TagService.GetPageAsync(_parameters);
        _tags = memberPage.Items;
        _pageView = memberPage.PageView;
    } 
}
