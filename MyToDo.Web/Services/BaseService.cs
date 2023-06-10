using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.Web.Authentication;
using Newtonsoft.Json;

namespace MyToDo.Web.Services;

internal abstract class BaseService
{
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
    
    protected readonly HttpClient HttpClient;
    
    protected BaseService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider =
            authenticationStateProvider as CustomAuthenticationStateProvider ?? throw new Exception();
        HttpClient = httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    protected abstract string BaseUrl { get; }
    
    protected async Task<HttpRequestMessage> CreateHttpRequestMessage(HttpMethod httpMethod, string url, object? body = null!)
    {
        var httpRequest = new HttpRequestMessage(httpMethod, url);

        if (httpMethod != HttpMethod.Get || body is null)
        {
            var json = JsonConvert.SerializeObject(body);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        await AddAuthorizationHeader(httpRequest);

        return httpRequest;
    }

    protected static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await HandleError(response, cancellationToken);
        }

        return await response.Content.ReadFromJsonAsync(typeof(T), cancellationToken: cancellationToken) is T value 
            ? Result.Success(value) 
            : Result.Failure(DomainErrors.FailedToDeserializeObject);
    }
    
    protected static async Task<Result> HandleResponse(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await HandleError(response, cancellationToken);
        }
        
        return Result.Success();
    }

    protected static async Task<Result> HandleError(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var error = response.StatusCode == HttpStatusCode.Forbidden
            ? DomainErrors.Forbidden
            : await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

        return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
    }

    private async Task AddAuthorizationHeader(HttpRequestMessage message)
    {
        // var token = await _authenticationStateProvider.GetToken();
        //
        // if (!message.Headers.Contains("Authorization"))
        // {
        //     message.Headers.Authorization ??= new AuthenticationHeaderValue("Bearer", token);
        // }

        // if (HttpClient.DefaultRequestHeaders.Authorization is null)
        // {
        //     var token = await _authenticationStateProvider.GetToken();
        //
        //     HttpClient.DefaultRequestHeaders.Authorization =
        //         new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
        // }
    }
}
