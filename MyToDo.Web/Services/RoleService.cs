using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Roles;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl = "roles";

    public RoleService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    public async Task<Result<List<RoleDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await _httpClient.SendAsync(request, cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

            return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
        }

        var roles = await response.Content.ReadFromJsonAsync<List<RoleDto>>(cancellationToken: cancellationToken);

        return roles is not null ? Result.Success(roles) : Result.Failure(DomainErrors.FailedToDeserializeObject);
    }
}