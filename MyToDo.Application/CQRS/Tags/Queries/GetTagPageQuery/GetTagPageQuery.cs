using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetTagPageQuery;

public record GetTagPageQuery(
    string? SearchString,
    int PageIndex,
    int PageSize) : IQuery<TagPagedListDto>;
