using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Web.Components.UpdateMember;

public partial class UpdateMember
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter]
    public MemberDto Member { get; set; }
    
    private UpdateMemberDto _updateMemberDto = new UpdateMemberDto();
    
    protected override async Task OnInitializedAsync()
    {
        var result = await MemberService.GetByIdAsync(Member.Id);
        var member = result.Value;
        
        _updateMemberDto.Email = member.Email;
        _updateMemberDto.FirstName = member.FirstName;
        _updateMemberDto.LastName = member.LastName;
        _updateMemberDto.Id = member.Id;
        _updateMemberDto.IsActive = member.IsActive;
    }

    private async Task UpdateAsync()
    {
        await MemberService.UpdateAsync(_updateMemberDto);
    }
}