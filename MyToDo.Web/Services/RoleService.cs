using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Roles;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class RoleService : BaseService, IRoleService
{
    public RoleService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string BaseUrl => "roles";

    public async Task<Result<List<RoleDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await HttpClient.SendAsync(request, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

            return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
        }

        var roles = await response.Content.ReadFromJsonAsync<List<RoleDto>>(cancellationToken: cancellationToken);

        return roles is not null ? Result.Success(roles) : Result.Failure(DomainErrors.FailedToDeserializeObject);
    }
}