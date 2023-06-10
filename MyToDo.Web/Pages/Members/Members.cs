using System.Runtime.CompilerServices;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Components;

namespace MyToDo.Web.Pages.Members;

public sealed partial class Members : BaseComponent
{
    private List<MemberDto> _members = new List<MemberDto>();
    private PageViewDto _pageView = new PageViewDto();

    private bool _isOrderByNameAsc = true;

    private bool _isShowRegisterForm = false;

    private bool _isShowUpdateForm = false;

    private bool _isShowStatisticsForm;

    private readonly MemberPageRequestDto _parameters = new MemberPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10
    };

    private MemberDto _updatedMember;

    private Guid _memberIdStatistics;
    
    protected override async Task OnInitializedAsync()
    {
        var getMemberPageResult = await MemberService.GetPageAsync(_parameters);
        if (getMemberPageResult.IsFailure)
        {
            ShowErrorDialog(getMemberPageResult.Error);
            return;
        }
        
        _members = getMemberPageResult.Value.Items;
        _pageView = getMemberPageResult.Value.PageView;
    }
    
    private async Task OnActActivityUpdateAsync(Guid memberId, bool isActive)
    {
        var updateActivityResult = await MemberService.UpdateActivityAsync(memberId, isActive);
        if (updateActivityResult.IsFailure)
        {
            ShowErrorDialog(updateActivityResult.Error);
        }

        await GetMemberPageAsync();
    }
    
    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }

    private async Task GetMemberPageAsync()
    {
        var getMemberPageResult = await MemberService.GetPageAsync(_parameters);
        if (getMemberPageResult.IsFailure)
        {
            ShowErrorDialog(getMemberPageResult.Error);
            return;
        }
        
        _members = getMemberPageResult.Value.Items;
        _pageView = getMemberPageResult.Value.PageView;
    }

    private void SortByName()
    {
        IOrderedEnumerable<MemberDto> _sortedMembers;

        _sortedMembers = _isOrderByNameAsc 
            ? _members.OrderBy(m => m.LastName) 
            : _members.OrderByDescending(m => m.LastName);

        _isOrderByNameAsc = !_isOrderByNameAsc;
        
        _members = _sortedMembers.ToList();
    }

    private async Task SearchAsync()
    {
        await GetMemberPageAsync();
    }

    private void ShowRegisterForm() => _isShowRegisterForm = true;

    private async Task CloseRegisterForm()
    {
       await GetMemberPageAsync();
        _isShowRegisterForm = false;
    }

    private void ShowUpdateForm(MemberDto memberDto)
    {
        _updatedMember = memberDto;
        _isShowUpdateForm = true;
    }

    private async Task CloseUpdateForm()
    {
        await GetMemberPageAsync();
        
        _updatedMember = null!;
        _isShowUpdateForm = false;
    }

    private void ShowStatisticsForm(Guid memberId)
    {
        _memberIdStatistics = memberId;
        _isShowStatisticsForm = true;
    }

    private void CloseStatisticsForm()
    {
        _memberIdStatistics = default;
        _isShowStatisticsForm = false;
    }
}
