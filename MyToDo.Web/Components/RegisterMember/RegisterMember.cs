using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;
using MyToDo.HttpContracts.Roles;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Components.RegisterMember;

public partial class RegisterMember : BaseComponent
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    private RegisterMemberDto _registerMemberDto = new RegisterMemberDto();

    private List<RoleDto> _roles = new();

    protected override async Task OnInitializedAsync()
    {
        var getRolesResult = await RoleService.GetAllAsync();

        _roles = getRolesResult.Value;
    }

    private async Task RegisterAsync()
    {
        var registerResult = await MemberService.RegisterAsync(_registerMemberDto);
        if (registerResult.IsFailure)
        {
            ShowErrorDialog(registerResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
}