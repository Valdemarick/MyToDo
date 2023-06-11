using MyToDo.HttpContracts.Members;
using MyToDo.Web.Authentication;

namespace MyToDo.Web.Pages.Login;

public partial class Login
{
    private readonly LoginDto _loginDto = new();

    private async Task LoginAsync()
    {
        var loginResult = await MemberService.LoginAsync(_loginDto);
                if (loginResult.IsFailure)
        {
            ShowErrorDialog(loginResult.Error);
            return;
        }

        var customAuthProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;

        await customAuthProvider.UpdateAuthenticationState(loginResult.Value);
        
        NavigationManager.NavigateTo("/", true);
    }
}