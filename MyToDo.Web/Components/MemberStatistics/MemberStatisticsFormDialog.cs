using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Web.Components.MemberStatistics;

public partial class MemberStatisticsFormDialog
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; } 
    
    [Parameter]
    public Guid MemberId { get; set; }

    private MemberStatisticsDto _memberStatisticsDto = new();

    protected override async Task OnInitializedAsync()
    {
        var getStatisticsResult = await MemberService.GetMemberStatisticsAsync(MemberId);
        if (getStatisticsResult.IsFailure)
        {
            ShowErrorDialog(getStatisticsResult.Error);
            return;
        }

        _memberStatisticsDto = getStatisticsResult.Value;
    }
}