using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetAllTagsQuery;

public record GetAllTagsQuery() : IQuery<List<TagDto>>;
