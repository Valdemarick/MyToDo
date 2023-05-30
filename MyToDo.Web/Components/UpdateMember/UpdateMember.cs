using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Members;
using MyToDo.HttpContracts.Roles;

namespace MyToDo.Web.Components.UpdateMember;

public partial class UpdateMember : BaseComponent
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter]
    public MemberDto Member { get; set; }
    
    private UpdateMemberDto _updateMemberDto = new UpdateMemberDto();

    private List<RoleDto> _roles = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await MemberService.GetByIdAsync(Member.Id);
        var member = result.Value;

        _updateMemberDto.Email = member.Email;
        _updateMemberDto.FirstName = member.FirstName;
        _updateMemberDto.LastName = member.LastName;
        _updateMemberDto.Id = member.Id;
        _updateMemberDto.RoleId = member.RoleId;
        _updateMemberDto.IsActive = member.IsActive;
        
        var getRolesResult = await RoleService.GetAllAsync();

        _roles = getRolesResult.Value;
    }
    
    private async Task UpdateAsync()
    {
        var updateResult = await MemberService.UpdateAsync(_updateMemberDto);
        if (updateResult.IsFailure)
        {
            ShowErrorDialog(updateResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
}