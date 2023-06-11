using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tasks.Queries.GetTaskTagsQuery;

public record GetTaskTagsQuery(Guid TaskId) : IQuery<List<TagDto>>;
