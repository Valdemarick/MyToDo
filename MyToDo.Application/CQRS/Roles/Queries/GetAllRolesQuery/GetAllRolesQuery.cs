using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Roles;

namespace MyToDo.Application.CQRS.Roles.Queries.GetAllRolesQuery;

public record GetAllRolesQuery() : IQuery<List<RoleDto>>;
