using System.Text;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using Newtonsoft.Json;

namespace MyToDo.Web.Services;

internal abstract class BaseService
{
    protected abstract string BaseUrl { get; }
    
    protected static HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string url, object? body = null!)
    {
        var httpRequest = new HttpRequestMessage(httpMethod, url);

        if (httpMethod != HttpMethod.Get || body is null)
        {
            var json = JsonConvert.SerializeObject(body);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return httpRequest;
    }

    protected static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await HandleError(response, cancellationToken);
        }

        var value = await response.Content.ReadFromJsonAsync(typeof(T), cancellationToken: cancellationToken);

        return value is T val ? Result.Success(val) : Result.Failure(DomainErrors.FailedToDeserializeObject);

        // return await response.Content.ReadFromJsonAsync(typeof(T), cancellationToken: cancellationToken) is T value 
        //     ? Result.Success(value) 
        //     : Result.Failure(DomainErrors.FailedToDeserializeObject);
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
        var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

        return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
    }
}