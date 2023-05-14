﻿using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class MemberService : IMemberService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client;
    
    private const string _baseUrl = "members";

    public MemberService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _client = _httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    public async Task<List<MemberDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var members = await _client.GetFromJsonAsync<List<MemberDto>>(_baseUrl, cancellationToken);

        return members ?? new List<MemberDto>();
    }
}
