using Microsoft.AspNetCore.Components.Authorization;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Roles;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class RoleService : BaseService, IRoleService
{
    public RoleService(IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authenticationStateProvider) : base(httpClientFactory, authenticationStateProvider)
    {
    }

    protected override string BaseUrl => "roles";

    public async Task<Result<List<RoleDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var request = await CreateHttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await HttpClient.SendAsync(request, cancellationToken: cancellationToken);

        return await HandleResponse<List<RoleDto>>(response, cancellationToken);
    }
}