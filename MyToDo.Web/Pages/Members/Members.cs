using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Web.Pages.Members;

public sealed partial class Members
{
    private List<MemberDto> _members = new List<MemberDto>();
    private PageViewDto _pageView = new PageViewDto();
    
    private readonly MemberPageRequestDto _parameters = new MemberPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10
    };

    protected override async Task OnInitializedAsync()
    {
        var memberPage = await MemberService.GetPageAsync(_parameters);
        _members = memberPage.Items;
        _pageView = memberPage.PageView;
    }

    private void RedirectToCreateMemberForm() => NavigationManager.NavigateTo("/members");

    private void RedirectToUpdateMemberForm() => NavigationManager.NavigateTo("/members");

    private async Task OnActActivityUpdateAsync(Guid memberId, bool isActive)
    {
        await MemberService.UpdateActivityAsync(memberId, isActive);
    }
    
    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }
    
    private async Task GetMemberPageAsync()
    {
        var memberPage = await MemberService.GetPageAsync(_parameters);
        _members = memberPage.Items;
        _pageView = memberPage.PageView;
    } 
}
