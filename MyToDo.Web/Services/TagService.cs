using MyToDo.Application.Common.Dtos.Tags;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TagService : ITagService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client;
    
    private const string _baseUrl = "tags";

    public TagService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _client = _httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    public async Task<List<TagDto>> GetAllTagsAsync(CancellationToken cancellationToken = default)
    {
        var allTags = await _client.GetFromJsonAsync<List<TagDto>>(_baseUrl, cancellationToken);
        
        return allTags ?? new List<TagDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _client.DeleteAsync($"{_baseUrl}/{id}", cancellationToken);
    }
}