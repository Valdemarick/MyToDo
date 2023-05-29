using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Services.Abstractions;


namespace MyToDo.Web.Components.RegisterMember;

public partial class RegisterMember
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }

    private RegisterMemberDto _registerMemberDto = new RegisterMemberDto();

    private async Task RegisterAsync()
    {
        await MemberService.CreateAsync(_registerMemberDto);
    }
}