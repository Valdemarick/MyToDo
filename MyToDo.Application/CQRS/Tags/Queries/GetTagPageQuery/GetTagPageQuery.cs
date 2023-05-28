using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetTagPageQuery;

public record GetTagPageQuery(TagPageRequestDto Parameters) : IQuery<TagPagedListDto>;
