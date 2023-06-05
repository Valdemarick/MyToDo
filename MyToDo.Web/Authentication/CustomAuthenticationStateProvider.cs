using System.Security.Claims;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Extensions;

namespace MyToDo.Web.Authentication;

internal sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ISessionStorageService _sessionStorageService;

    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    
    public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSession = await _sessionStorageService.GetItemFromSessionStorageAsync<MemberSessionDto>("Session");
            if (userSession is null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.FullName),
                new Claim(ClaimTypes.Role, userSession.Role)
            }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claims));
        }
        catch (Exception e)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(MemberSessionDto dto)
    {
        ClaimsPrincipal? claimsPrincipal = null!;

        if (dto is not null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, dto.FullName),
                new Claim(ClaimTypes.Role, dto.Role),
            }));
            dto.ExpiryTimeStamp = DateTime.Now.AddSeconds(dto.ExpiresIn);
            
            await _sessionStorageService.SaveItemToSessionStorageAsync("Session", dto);
        }
        else
        {
            claimsPrincipal = _anonymous;
            await _sessionStorageService.RemoveItemAsync("Session");
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult<AuthenticationState>(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<string> GetToken()
    {
        var result = string.Empty;

        try
        {
            var session = await _sessionStorageService.GetItemFromSessionStorageAsync<MemberSessionDto>("Session");
            if (session is not null && DateTime.Now < session.ExpiryTimeStamp)
            {
                result = session.Token;
            }
        }
        catch (Exception e)
        {
        }

        return result;
    }
}
