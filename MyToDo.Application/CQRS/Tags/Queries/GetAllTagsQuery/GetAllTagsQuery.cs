using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetAllTagsQuery;

public record GetAllTagsQuery() : IQuery<List<TagDto>>;
